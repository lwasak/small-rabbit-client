using System.ComponentModel;
using Spectre.Console.Cli;

namespace RabbitMq.Client.Console;

public sealed class Settings : CommandSettings
{
    [Description("Hostname for rebbitmq connection")]
    [CommandOption("-h|--hostname")]
    public required string Host { get; init; }
    
    [Description("RabbitMq queue name to wich should connect")]
    [CommandOption("-q|--queue")]
    public required string QueueName { get; init; }
    
    [Description("RabbitMq username")]
    [CommandOption("-u|--username")]
    public string? User { get; init; }
    
    [Description("RabbitMq user password")]
    [CommandOption("-p|--password")]
    public string? Password { get; init; }
}