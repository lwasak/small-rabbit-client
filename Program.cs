using RabbitMQ.Client;
using RabbitMq.Client.Console;
using Spectre.Console.Cli;

var app = new CommandApp<ConsumeQueueCommand>();

await app.RunAsync(args);