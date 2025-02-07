using System.Text;
using Newtonsoft.Json;
using OrderManagement.Model.Orders;
using OrderManagement.Utility.Api;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// ReSharper disable InconsistentNaming

namespace OrderManagement.Processor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        //hard coded for demo
        public static readonly Uri MQUri = new Uri("amqp://guest:guest@localhost:5672");
        public static readonly string MQClientProvideName = "Order Management MQ Processor";
        public static readonly string MQExchange = "Order Management Exchange";
        public static readonly string MQQueu = "Order Management Queue";
        public static readonly string MQRoutingKey = "Order-Management-Routing-Key";

        private ConnectionFactory? _connectionFactory;
        private IConnection? _connection;
        private IChannel? _channel;
        private string? _consumerTag;

        public static string ApiUrl = "https://localhost:7221/";

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.Uri = MQUri;
            _connectionFactory.ClientProvidedName = MQClientProvideName;

            _connection = await _connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(MQExchange, ExchangeType.Direct);
            await _channel.QueueDeclareAsync(MQQueu, false, false, false);
            await _channel.QueueBindAsync(MQQueu, MQExchange, MQRoutingKey);
            await _channel.BasicQosAsync(0, 1, false);

            await base.StartAsync(cancellationToken);
            //return;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(_consumerTag) && _channel != null)
                await _channel.BasicCancelAsync(_consumerTag);
            if(_channel != null)
                await _channel.CloseAsync();
            if(_connection != null)
                await _connection.CloseAsync();

            await base.StopAsync(cancellationToken);
            //return;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_channel is null)
                return;

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (_, args) =>
            {
                await Task.Delay(5000); //simulate long job

                var bytes = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(bytes);
                var order = JsonConvert.DeserializeObject<OrderDto>(message);
                if (order is null)
                {
                    await _channel.BasicRejectAsync(args.DeliveryTag, false);
                }
                else
                {
                    order.Status = "Approved";

                    var restApi = new RestApi();
                    await restApi.Call(ApiUrl + "v1/order/process", HttpMethod.Post, order);
                    
                    await _channel.BasicAckAsync(args.DeliveryTag, false);
                }
            };

            _consumerTag = await _channel.BasicConsumeAsync(MQQueu, false, consumer);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
