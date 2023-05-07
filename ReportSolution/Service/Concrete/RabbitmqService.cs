using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service.Abstract;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Service.Concrete
{
    internal class RabbitmqService : IRabbitmqService
    {
        private readonly IModel _channel;
        //private readonly EventingBasicConsumer _consumer;
        private const string _queueName = "report";
        private const string _routingKey = "report";

        public RabbitmqService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5673
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(_queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            //_consumer = new EventingBasicConsumer(_channel);
            //_consumer.Received += (model, ea) =>
            //{
            //    var body = ea.Body.ToArray();
            //    var message = Encoding.UTF8.GetString(body);
            //    var a = 5;
            //};
            //_channel.BasicConsume(queue: _queueName, autoAck: true, consumer: _consumer);
            //_channel.BasicQos(0, 1, false);
        }

        public void ProduceMessage<T>(T message)
        {
            var body = Encoding.UTF8.GetBytes(message.ToString());
            _channel.BasicPublish(exchange: "", routingKey: _routingKey, body: body);
        }

        public string ConsumeMessage()
        {

            return "";
        }
    }
}