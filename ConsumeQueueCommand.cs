using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;

namespace RabbitMq.Client.Console;

public class ConsumeQueueCommand : Command<Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.MarkupLine($"Connecting to [yellow]{settings.QueueName}[/] on [yellow]{settings.Host}[/]...");

        var factory = new ConnectionFactory
        {
            HostName = settings.Host,
            UserName = settings.User,
            Password = settings.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += OnMessageReceived;
        channel.BasicConsume(queue: settings.QueueName, autoAck: true, consumer: consumer);
        
        AnsiConsole.MarkupLine($"Consuming messages... press [yellow]Q[/] to quit");
        
        var cki = new ConsoleKeyInfo();
        do 
        {
            // true hides the pressed character from the console
            cki = System.Console.ReadKey(true);
    
            // Wait for an ESC
        } while (cki.Key != ConsoleKey.Q);

        return 0;
    }

    private void OnMessageReceived(object? sender, BasicDeliverEventArgs basicDeliverEventArgs)
    {
        var body = basicDeliverEventArgs.Body.ToArray();
        var json = new JsonText(Encoding.UTF8.GetString(body));
        AnsiConsole.Write(
            new Panel(json)
            .Collapse()
            .RoundedBorder()
            .BorderColor(Color.Yellow));
    }
}