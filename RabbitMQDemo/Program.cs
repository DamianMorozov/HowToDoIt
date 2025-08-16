Console.WriteLine("RabbitMQ Demo");

Console.WriteLine("Press 's' to send a message, 'r' to receive messages, or any other key to exit.");
var menu = string.Empty;
while (menu != "Exit")
{
    menu = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Choose an action:")
            .AddChoices(["Send message", "Receive messages", "Exit"]));
    switch (menu)
    {
        case "Send message":
            Console.WriteLine("You selected to send a message.");
            var message = Console.ReadLine() ?? string.Empty;
            await RaProducer.SendMessageAsync(message, "demoQueue");
            break;
        case "Receive messages":
            Console.WriteLine("You selected to receive messages.");
            await RaConsumer.ReceiveMessagesAsync("demoQueue");
            break;
        case "Exit":
            Console.WriteLine("Exiting RabbitMQ Demo.");
            return;
        default:
            break;
    }
}
