using Producer.amqp;
using Producer.Orders;

namespace Producer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var contador = 1;
            while (contador <= 1000)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation($"Contador nº {contador}");

                var order = new Order()
                {
                    Id = contador,
                    Description = $"Pedido numero {contador}"
                };

                var payment = new PaymentAMQPConfig();
                payment.Publish(order, "payments-queue");

                contador++;

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}