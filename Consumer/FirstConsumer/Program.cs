using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//var factory = new ConnectionFactory() { HostName = "localhost" };
var factory = new ConnectionFactory() { HostName = "firstrabbitmqapp" }; // host name for docker

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "letterbox",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new EventingBasicConsumer(channel);

var random = new Random();

consumer.Received += (model, ea) =>
{
    var processingTime = random.Next(100, 300);

    var body = ea.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Recieved: '{message}', will take {processingTime} to process");

    Task.Delay(TimeSpan.FromMilliseconds(processingTime)).Wait();

    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};

channel.BasicConsume(queue: "letterbox", autoAck: false, consumer: consumer);

Console.WriteLine("Consuming");

//Console.ReadKey();
Console.Read();