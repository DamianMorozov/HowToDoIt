using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var cts = new CancellationTokenSource();
// Ctrl+C — clean exit
Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

while (true)
{
    Console.Clear();
    Console.WriteLine("📡 Connecting to WebSocket server...");
    using var socket = new ClientWebSocket();

    try
    {
        await socket.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
        Console.WriteLine("✅ Connected to server.");
    }
    catch (WebSocketException ex)
    {
        Console.WriteLine($"❌ Connection failed: {ex.Message}");
        Console.WriteLine("🔁 Retry in 3 seconds or press Ctrl+C to exit...");
        await Task.Delay(3000);
        continue;
    }

    Console.WriteLine("✅ Connected. Choose an option:");
    Console.WriteLine("1 - Streaming Text Protocol");
    Console.WriteLine("2 - Streaming Binary Protocol");
    Console.WriteLine("3 - Send JSON Message");
    Console.WriteLine("4 - Exit");
    Console.Write("Enter choice: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await SendTextMessageAsync(socket);
            break;
        case "2":
            await SendBinaryAsync(socket);
            break;
        case "3":
            await SendSingleMessageAsync(socket);
            break;
        case "4":
            Console.WriteLine("👋 Exiting...");
            return;
        default:
            Console.WriteLine("❌ Invalid choice.");
            break;
    }

    Console.WriteLine("🔄 Press Enter to restart or type 'exit' to quit.");
    var restart = Console.ReadLine();
    if (restart?.ToLower() == "exit")
        break;
}

static async Task SendTextMessageAsync(ClientWebSocket socket)
{
    try
    {
        // Sending a message (Dispose completes EndOfMessage)
        using (var writeStream = WebSocketStream.CreateWritableMessageStream(socket, WebSocketMessageType.Text))
        using (var writer = new StreamWriter(writeStream))
        {
            Console.Write("Enter text to send: ");
            var text = Console.ReadLine();
            await writer.WriteLineAsync(text);
            await writer.FlushAsync();
            Console.WriteLine("✅ Text sent and flushed.");
        }

        // Reading the answer
        if (socket.State == WebSocketState.Open || socket.State == WebSocketState.CloseReceived)
        {
            try
            {
                using var readStream = WebSocketStream.CreateReadableMessageStream(socket);
                using var reader = new StreamReader(readStream, new UTF8Encoding(false));
                var reply = await reader.ReadToEndAsync();
                Console.WriteLine($"Server: {reply}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("⏳ Timeout: No response from server.");
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"❌ Receive failed: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"⚠️ Skipping receive: WebSocket state is {socket.State}");
        }

        // Correct closure
        if (socket.State is WebSocketState.Open or WebSocketState.CloseReceived)
        {
            try
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
                Console.WriteLine("🔒 Connection closed gracefully.");
            }
            catch (WebSocketException wex)
            {
                Console.WriteLine($"CloseAsync failed: {wex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"⚠️ Skip CloseAsync: State = {socket.State}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"💥 Unexpected error: {ex.Message}");
    }
}

static async Task SendBinaryAsync(ClientWebSocket socket)
{
    using var stream = WebSocketStream.Create(socket, WebSocketMessageType.Binary, ownsWebSocket: false);
    var data = new byte[] { 0x01, 0x02, 0x03, 0x04 };
    await stream.WriteAsync(data);
    await stream.FlushAsync();
    Console.WriteLine($"✅ Binary sent: {BitConverter.ToString(data)}");
}

static async Task SendSingleMessageAsync(ClientWebSocket socket)
{
    var message = new AppMessage { Text = "Hello, server!" };
    using var jsonStream = WebSocketStream.CreateWritableMessageStream(socket, WebSocketMessageType.Text);
    await JsonSerializer.SerializeAsync(jsonStream, message);
    Console.WriteLine("✅ JSON message sent.");
}

public class AppMessage
{
    public string? Text
    {
        get; set;
    }
}
