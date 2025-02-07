
using System.Text;
using Newtonsoft.Json;
using OrderManagement.Model;
using RabbitMQ.Client;

// ReSharper disable InconsistentNaming

namespace OrderManagement.Business
{
    public abstract class BaseMessageBusiness
    {
        //hard coded for demo
        public static readonly Uri MQUri = new Uri("amqp://guest:guest@localhost:5672");
        public static readonly string MQClientProvideName = "Order Management MQ";
        public static readonly string MQExchange = "Order Management Exchange";
        public static readonly string MQQueu = "Order Management Queue";
        public static readonly string MQRoutingKey = "Order-Management-Routing-Key";

        private ConnectionFactory? _connectionFactory;
        private IConnection? _connection;
        private IChannel? _channel;

        public async Task Send<T>(T message) where T : BaseDto
        {
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.Uri = MQUri;
            _connectionFactory.ClientProvidedName = MQClientProvideName;

            _connection = await _connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(MQExchange, ExchangeType.Direct);
            await _channel.QueueDeclareAsync(MQQueu, false, false, false);
            await _channel.QueueBindAsync(MQQueu, MQExchange, MQRoutingKey);

            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            await _channel.BasicPublishAsync(MQExchange, MQRoutingKey, true, bytes);

            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }
    }
}
