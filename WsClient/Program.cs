using System.Net.WebSockets;
using System.Text;

Console.WriteLine("Connecting to ws://localhost:5000/ws ...");

using var ws = new ClientWebSocket();
var cts = new CancellationTokenSource();

// Ctrl+C — clean exit
Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

try
{
    await ws.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
    Console.WriteLine("Connected. Type a message and press Enter. Type /exit to quit.");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect: {ex.Message}");
    return;
}

// Background reception
var receiveTask = Task.Run(async () =>
{
    var buffer = new byte[4 * 1024];

    try
    {
        while (ws.State == WebSocketState.Open && !cts.IsCancellationRequested)
        {
            var result = await ws.ReceiveAsync(buffer, cts.Token);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine($"Server initiated close: {ws.CloseStatus} {ws.CloseStatusDescription}");
                break;
            }

            var bytes = new List<byte>(result.Count);
            bytes.AddRange(buffer.AsSpan(0, result.Count).ToArray());

            while (!result.EndOfMessage)
            {
                result = await ws.ReceiveAsync(buffer, cts.Token);
                bytes.AddRange(buffer.AsSpan(0, result.Count).ToArray());
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[recv] {Encoding.UTF8.GetString(bytes.ToArray())}");
            Console.ResetColor();
        }
    }
    catch (OperationCanceledException) { /* graceful */ }
    catch (Exception ex)
    {
        Console.WriteLine($"Receive error: {ex.Message}");
    }
});

// Sending from the console
while (ws.State == WebSocketState.Open && !cts.IsCancellationRequested)
{
    var line = Console.ReadLine();
    if (line is null) continue;

    if (line.Equals("/exit", StringComparison.OrdinalIgnoreCase))
        break;

    var payload = Encoding.UTF8.GetBytes(line);
    try
    {
        await ws.SendAsync(payload, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Send error: {ex.Message}");
        break;
    }
}

// Correct closure
if (ws.State == WebSocketState.Open)
{
    try
    {
        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client exit", CancellationToken.None);
    }
    catch { /* ignore */ }
}

cts.Cancel();
await receiveTask;

Console.WriteLine("Closed.");