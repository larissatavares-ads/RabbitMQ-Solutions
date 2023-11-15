using Producer.Orders;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Producer.amqp
{
    public class PaymentAMQPConfig
    {
        public void Publish(Order order, string queueName)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonSerializer.Serialize(order);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: string.Empty,
                routingKey: queueName,
                basicProperties: properties,
                body: body);

            Console.WriteLine($" [x] Sent {message}");
        }
    }
}
