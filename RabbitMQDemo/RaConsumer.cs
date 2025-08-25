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

        await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        // 🔑 IMPORTANT: Limit prefetchCount to 1
        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"[{DateTime.Now}] Received message: {message}");

            // Simulate work
            var spaceCount = message.Split(' ').Length - 1;
            await Task.Delay(spaceCount * 1_000);

            Console.WriteLine("[{DateTime.Now}] Done");
            
            // Here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
            await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer);

        // Press any key to exit
        Console.WriteLine("Press [enter] to exit.");
        Console.ReadLine();
    }
}
