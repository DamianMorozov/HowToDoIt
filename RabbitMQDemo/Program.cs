Console.WriteLine("RabbitMQ Demo");

var queueName = "taskQueue";
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
            await RaProducer.SendMessageAsync(queueName);
            break;
        case "Receive messages":
            Console.WriteLine("You selected to receive messages.");
            await RaConsumer.ReceiveMessagesAsync(queueName);
            break;
        case "Exit":
            Console.WriteLine("Exiting RabbitMQ Demo.");
            return;
        default:
            break;
    }
}
