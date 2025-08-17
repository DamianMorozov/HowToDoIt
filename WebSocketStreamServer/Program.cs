using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();
app.UseWebSockets();

var clients = new ConcurrentDictionary<Guid, WebSocket>();

app.Map("/ws", async context =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = 400;
        return;
    }

    using var socket = await context.WebSockets.AcceptWebSocketAsync();
    var clientId = Guid.NewGuid();
    clients.TryAdd(clientId, socket);
    Console.WriteLine($"Client connected: {clientId}");

    try
    {
        // 1. Streaming Text Protocol
        using var textStream = WebSocketStream.Create(socket, WebSocketMessageType.Text, ownsWebSocket: false);
        using var reader = new StreamReader(textStream, leaveOpen: true);
        var line = await reader.ReadLineAsync();
        Console.WriteLine($"[Text] {line}");

        // 2. Streaming Binary Protocol
        var binaryBuffer = new byte[4];
        await textStream.ReadExactlyAsync(binaryBuffer);
        Console.WriteLine($"[Binary] {BitConverter.ToString(binaryBuffer)}");

        // 3. Read Single Message (JSON)
        using var jsonStream = WebSocketStream.CreateReadableMessageStream(socket);
        var msg = await JsonSerializer.DeserializeAsync<AppMessage>(jsonStream);
        Console.WriteLine($"[JSON] {msg?.Text}");

        // Broadcast JSON to all clients
        foreach (var kvp in clients)
        {
            if (kvp.Value.State == WebSocketState.Open)
            {
                using var broadcastStream = WebSocketStream.CreateWritableMessageStream(kvp.Value, WebSocketMessageType.Text);
                await JsonSerializer.SerializeAsync(broadcastStream, new AppMessage { Text = $"Echo from {clientId}: {msg?.Text}" });
            }
        }

        // 4. Write Single Message (Binary)
        using var writeStream = WebSocketStream.CreateWritableMessageStream(socket, WebSocketMessageType.Binary);
        await writeStream.WriteAsync(new byte[] { 0xDE, 0xAD, 0xBE, 0xEF });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error with client {clientId}: {ex.Message}");
    }
    finally
    {
        Console.WriteLine($"[State] WebSocket state before cleanup: {socket.State}");
        clients.TryRemove(clientId, out _);
        if (socket.State == WebSocketState.Open ||
            socket.State == WebSocketState.CloseReceived ||
            socket.State == WebSocketState.CloseSent)
        {
            try
            {
                Console.WriteLine("[Action] Attempting CloseAsync...");
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                Console.WriteLine("[Success] CloseAsync completed.");
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"[Error] CloseAsync failed: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("[Skip] CloseAsync skipped due to invalid state.");
        }
        Console.WriteLine($"Client disconnected: {clientId}");
    }
});

await app.RunAsync();

public class AppMessage
{
    public string? Text
    {
        get; set;
    }
}
