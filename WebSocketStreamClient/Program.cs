using System.Net.WebSockets;
using System.Text.Json;

while (true)
{
    Console.WriteLine("📡 Connecting to WebSocket server...");
    using var socket = new ClientWebSocket();
    await socket.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);

    Console.WriteLine("✅ Connected. Choose an option:");
    Console.WriteLine("1 - Streaming Text Protocol");
    Console.WriteLine("2 - Streaming Binary Protocol");
    Console.WriteLine("3 - Send JSON Message");
    Console.WriteLine("4 - Receive Binary Message");
    Console.WriteLine("5 - Exit");
    Console.Write("Enter choice: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            using (var stream = WebSocketStream.Create(socket, WebSocketMessageType.Text, ownsWebSocket: false))
            using (var writer = new StreamWriter(stream, leaveOpen: true))
            {
                Console.Write("Enter text to send: ");
                var text = Console.ReadLine();
                await writer.WriteLineAsync(text);
                Console.WriteLine("✅ Text sent.");
            }
            break;

        case "2":
            using (var stream = WebSocketStream.Create(socket, WebSocketMessageType.Binary, ownsWebSocket: false))
            {
                var data = new byte[] { 0x01, 0x02, 0x03, 0x04 };
                await stream.WriteAsync(data);
                Console.WriteLine($"✅ Binary sent: {BitConverter.ToString(data)}");
            }
            break;

        case "3":
            var message = new AppMessage { Text = "Hello, server!" };
            using (var jsonStream = WebSocketStream.CreateWritableMessageStream(socket, WebSocketMessageType.Text))
            {
                await JsonSerializer.SerializeAsync(jsonStream, message);
                Console.WriteLine("✅ JSON message sent.");
            }
            break;

        case "4":
            using (var binaryStream = WebSocketStream.CreateReadableMessageStream(socket))
            {
                var buffer = new byte[1024];
                int bytesRead;
                Console.WriteLine("📥 Waiting for binary message...");
                while ((bytesRead = await binaryStream.ReadAsync(buffer)) > 0)
                {
                    Console.WriteLine($"📦 Received {bytesRead} bytes: {BitConverter.ToString(buffer, 0, bytesRead)}");
                }
            }
            break;

        case "5":
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

public class AppMessage
{
    public string? Text
    {
        get; set;
    }
}
