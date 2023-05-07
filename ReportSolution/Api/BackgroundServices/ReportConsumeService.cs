using Api.Models;
using ClosedXML.Excel;
using Enums;
using Model.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

                    var client = new RestClient("http://localhost:5164");
                    var request = new RestRequest($"/api/contact/getreport", Method.Get);
                    var reportData = client.Execute<List<ReportDetailDto>>(request).Data;

                    if (reportData != null)
                    {
                        using var workBook = new XLWorkbook();
                        var workSheet = workBook.AddWorksheet("Rapor");
                        workSheet.Cell(1, 1).Value = "Location";
                        workSheet.Cell(1, 2).Value = "ContactCount";
                        workSheet.Cell(1, 3).Value = "PhoneNumberCount";
                        int row = 2;

                        foreach (ReportDetailDto data in reportData)
                        {
                            workSheet.Cell(row, 1).Value = data.Location;
                            workSheet.Cell(row, 2).Value = data.ContactCount;
                            workSheet.Cell(row, 3).Value = data.PhoneNumberCount;

                            row++;
                        }

                        string path = AppDomain.CurrentDomain.BaseDirectory + "reports";
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        workBook.SaveAs($"{path}/report_{reportId}.xlsx");

                        Report report = _reportService.GetById(Guid.Parse(reportId));
                        report.Status = ReportStatus.Completed;
                        _reportService.Update(report);
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