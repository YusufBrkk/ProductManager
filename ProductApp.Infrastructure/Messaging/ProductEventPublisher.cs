using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProductApp.Infrastructure.Messaging
{
    public class ProductEventPublisher
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "product-events";

        public void Publish(object eventObj)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = JsonSerializer.Serialize(eventObj);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }
    }
}