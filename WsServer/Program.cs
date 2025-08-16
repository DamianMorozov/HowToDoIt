using System.Net.WebSockets;
using System.Collections.Concurrent;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// We are clearly listening to HTTP (ws://) so as not to bother with certificates for wss://
builder.WebHost.UseUrls("http://localhost:5000");
var app = builder.Build();

// Ping/pong at the TCP level
app.UseWebSockets(new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(30) });

// Collection of active clients for broadcast
var clients = new ConcurrentDictionary<string, WebSocket>();

app.Map("/ws", async context =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Only WebSocket is supported here.");
        return;
    }

    using var socket = await context.WebSockets.AcceptWebSocketAsync();
    var id = Guid.NewGuid().ToString();
    clients[id] = socket;

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"[+] Connected: {id}");
    Console.ResetColor();

    var buffer = new byte[4 * 1024];

    try
    {
        while (socket.State == WebSocketState.Open)
        {
            // Reception (taking into account possible fragmentation)
            var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine($"[i] Client requested close: {id} ({socket.CloseStatus})");
                await socket.CloseAsync(socket.CloseStatus ?? WebSocketCloseStatus.NormalClosure,
                                        socket.CloseStatusDescription, CancellationToken.None);
                break;
            }

            var messageBytes = new List<byte>(result.Count);
            messageBytes.AddRange(buffer.AsSpan(0, result.Count).ToArray());

            while (!result.EndOfMessage)
            {
                result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                messageBytes.AddRange(buffer.AsSpan(0, result.Count).ToArray());
            }

            var message = Encoding.UTF8.GetString(messageBytes.ToArray());
            Console.WriteLine($"[{id}] {message}");

            // Echo response
            var echo = Encoding.UTF8.GetBytes($"echo: {message}");
            await socket.SendAsync(echo, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);

            // Simple broadcast to others (optional)
            foreach (var (otherId, otherSocket) in clients)
            {
                if (otherId == id || otherSocket.State != WebSocketState.Open) continue;
                var payload = Encoding.UTF8.GetBytes($"[{id}] {message}");
                try
                {
                    await otherSocket.SendAsync(payload, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch
                {
                    // Ignore delivery failures to individual clients
                    Console.WriteLine($"[!] Failed to send message to {otherId}");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[!] Error with {id}: {ex.Message}");
        Console.ResetColor();
    }
    finally
    {
        clients.TryRemove(id, out _);
        if (socket.State != WebSocketState.Closed && socket.State != WebSocketState.Aborted)
        {
            try
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server cleanup", CancellationToken.None);
            }
            catch { /* ignore */ }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[-] Disconnected: {id}");
        Console.ResetColor();
    }
});

app.MapGet("/", () => "WebSocket server is running. Connect to ws://localhost:5000/ws");

await app.RunAsync();
