namespace RabbitMQDemo;

/// <summary> Receives messages from a RabbitMQ queue </summary>
internal sealed class RaConsumer
{
    /// <summary> Receives messages from the specified RabbitMQ queue </summary>
    public static async Task ReceiveMessagesAsync(string queueName)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"[{DateTime.Now}] Received message: {message}");
            await Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
    }
}
