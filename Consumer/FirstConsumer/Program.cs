using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Task.Delay(10000).Wait();
var factory = new ConnectionFactory()
{
    HostName = "firstrabbitmqapp",  // host name for docker
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "letterbox",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

// channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new EventingBasicConsumer(channel);

var random = new Random();

consumer.Received += (model, ea) =>
{
    try
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Recieved: '{message}'");

        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }
    catch (System.Exception ex)
    {
        Console.WriteLine("NOT OK!!!");
        throw;
    }
};

channel.BasicConsume(queue: "letterbox", autoAck: true, consumer: consumer);

Console.WriteLine("Consuming");
