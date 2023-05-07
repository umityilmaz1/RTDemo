using Api.Models;
using ClosedXML.Excel;
using Helper;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RestSharp;
using Service.Abstract;
using System.Text;

namespace Api.BackgroundServices
{
    public class ReportConsumeService : BackgroundService
    {
        private readonly ILogger<ReportConsumeService> _logger;
        private readonly IReportService _reportService;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private const string _queueName = "report";

        public ReportConsumeService(ILogger<ReportConsumeService> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5673,
                DispatchConsumersAsync = true
            };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclarePassive(_queueName);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (bc, ea) =>
            {
                var reportId = Encoding.UTF8.GetString(ea.Body.ToArray());
                try
                {

                    var client = new RestClient("localhost:5164");
                    var request = new RestRequest($"api/contact/getreport/{reportId}", Method.Get);
                    var queryResult = client.Execute<ReportDetailDto>(request).Data;

                    if (queryResult != null)
                    {
                        using var workBook = new XLWorkbook();
                        var workSheet = workBook.AddWorksheet("Rapor");
                        workSheet.Cell("A1").Value = "Location";
                        workSheet.Cell("A2").Value = "ContactCount";
                        workSheet.Cell("A3").Value = "PhoneNumberCount";
                        workSheet.Cell("B1").Value = queryResult.Location;
                        workSheet.Cell("B2").Value = queryResult.ContactCount;
                        workSheet.Cell("B3").Value = queryResult.PhoneNumberCount;

                        if (!Directory.Exists("~/reports"))
                            Directory.CreateDirectory("~/reports");

                        workBook.SaveAs($"~/reports/report_{reportId}.xlsx");
                    }

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    _logger.LogError(default, e, e.Message);
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
        }
    }
}