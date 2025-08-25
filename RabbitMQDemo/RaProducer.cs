namespace RabbitMQDemo;

/// <summary> Sends messages to a RabbitMQ queue </summary>
internal sealed class RaProducer
{
    /// <summary> Sends a message to the specified RabbitMQ queue </summary>
    public static async Task SendMessageAsync(string queueName)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var message = string.Empty;
        do
        {
            Console.WriteLine($"Type message: ");
            message = Console.ReadLine() ?? string.Empty;
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties { Persistent = true };
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, mandatory: true, basicProperties: properties, body);
            Console.WriteLine($"[{DateTime.Now}] Sent message: {message}");

        } while (!string.IsNullOrEmpty(message));

        await channel.CloseAsync();
    }
}
