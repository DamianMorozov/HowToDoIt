using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
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

    var socket = await context.WebSockets.AcceptWebSocketAsync();
    var clientId = Guid.NewGuid();
    clients.TryAdd(clientId, socket);
    Console.WriteLine($"Client connected: {clientId}");

    try
    {
        // Read messages in a stream, one after another, while the socket is open
        while (socket.State == WebSocketState.Open)
        {
            if (args.Contains("-text") || args.Length == 0)
            {
                await ReceiveTextMessageAsync(socket);
            }
            if (args.Contains("-binary"))
            {
                await ReceiveBinaryAsync(socket);
            }
        }
    }
    catch (WebSocketException wex)
    {
        Console.WriteLine($"WebSocket error: {wex.Message}");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Operation canceled.");
    }
    finally
    {
        if (socket.State is WebSocketState.Open or WebSocketState.CloseReceived)
        {
            try
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server closing", CancellationToken.None);
                Console.WriteLine("Server closed gracefully.");
            }
            catch (WebSocketException wex)
            {
                Console.WriteLine($"CloseAsync error: {wex.Message}");
            }
        }
        Console.WriteLine("Client disconnected.");
    }
});

await app.RunAsync();

static async Task ReceiveTextMessageAsync(WebSocket socket)
{
    // Reading the incoming message in its entirety
    using var readStream = WebSocketStream.CreateReadableMessageStream(socket);
    using var reader = new StreamReader(readStream, new UTF8Encoding(false), detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: false);

    // ReadToEndAsync waits for EndOfMessage
    var text = await reader.ReadToEndAsync();
    // Trim last EOL
    text = text.TrimEnd('\r').TrimEnd('\n');
    if (string.IsNullOrEmpty(text)) return;
    // If the client sent an empty string, continue the cycle
    Console.Write("[Receive] ");
    Console.WriteLine(text);

    // Send the response and correctly terminate the message with Dispose.
    using var writeStream = WebSocketStream.CreateWritableMessageStream(socket, WebSocketMessageType.Text);
    using var writer = new StreamWriter(writeStream, new UTF8Encoding(false)) { AutoFlush = true };
    await writer.WriteAsync($"Echo: {text}");
    // writer.Dispose() => completes the frame with EndOfMessage = true
}

static async Task ReceiveBinaryAsync(WebSocket socket)
{
    using var binaryStream = WebSocketStream.CreateReadableMessageStream(socket);
    var binaryBuffer = new byte[4];
    await binaryStream.ReadExactlyAsync(binaryBuffer);
    Console.WriteLine($"[Binary] {BitConverter.ToString(binaryBuffer)}");

    // Reply with a binary message
    using var writeStream = WebSocketStream.CreateWritableMessageStream(socket, WebSocketMessageType.Binary);
    await writeStream.WriteAsync(new byte[] { 0xDE, 0xAD, 0xBE, 0xEF });
}

public class AppMessage
{
    public string? Text
    {
        get; set;
    }
}
