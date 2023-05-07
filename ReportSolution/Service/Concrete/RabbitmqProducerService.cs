using RabbitMQ.Client;
using Service.Abstract;
using System.Text;

namespace Service.Concrete
{
    internal class RabbitmqProducerService : IRabbitmqProducerService
    {
        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5673
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("report", exclusive: false);
            var body = Encoding.UTF8.GetBytes(message.ToString());
            channel.BasicPublish(exchange: "", routingKey: "report", body: body);
        }
    }
}