namespace RabbitMQDemo;

/// <summary> Sends messages to a RabbitMQ queue </summary>
internal sealed class RaProducer
{
    /// <summary> Sends a message to the specified RabbitMQ queue </summary>
    public static async Task SendMessageAsync(string message, string queueName)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
        Console.WriteLine($"[{DateTime.Now}] Sent message: {message}");

        await Task.CompletedTask;
    }
}
