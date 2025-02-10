using System.Text;
using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

public class RabbitMQEventPublisher : IEventPublisher
{
    private readonly string _hostname;
    private readonly string _username;
    private readonly string _password;

    public RabbitMQEventPublisher(IConfiguration configuration)
    {
        _hostname = configuration["RabbitMQ:HostName"];
        _username = configuration["RabbitMQ:UserName"];
        _password = configuration["RabbitMQ:Password"];
    }

    public async void Publish<T>(T @event)
    {
        var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };
        var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        _ = channel.QueueDeclareAsync(queue: typeof(T).Name, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "SalesCreation",
            mandatory: false,
            body: body
        );
    }
}
