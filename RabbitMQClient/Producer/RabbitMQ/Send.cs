using RabbitMQ.Client;
using System.Text;

namespace Producer.RabbitMQ
{
    public class Send
    {
        public void SendMessage()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                routingKey: "hello",
                basicProperties: null,
                body: body);

            Console.WriteLine($" [x] Sent {message}");
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
