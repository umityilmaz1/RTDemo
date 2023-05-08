using Api.Models;
using AutoMapper;
using ClosedXML.Excel;
using ClosedXML.Graphics;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Service.Abstract;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly ILogger<ReportController> _logger;
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;
        private readonly IRabbitmqService _rabbitmqProducerService;

        public ReportController(ILogger<ReportController> logger, IMapper mapper, IReportService reportService, IRabbitmqService rabbitmqProducerService)
        {
            _logger = logger;
            _mapper = mapper;
            _reportService = reportService;
            _rabbitmqProducerService = rabbitmqProducerService;
        }

        [HttpPost(nameof(RequestReport))]
        public IActionResult RequestReport()
        {
            Report report = new();
            _reportService.Create(report);
            _rabbitmqProducerService.ProduceMessage(report.Id);
            return Ok();
        }


        [HttpGet(nameof(GetReports))]
        public IActionResult GetReports()
        {
            IList<Report> reports = _reportService.GetList().ToList();
            IList<ReportListDto> reportsDto = _mapper.Map<IList<ReportListDto>>(reports);
            return new JsonResult(reportsDto);
        }

        [HttpGet(nameof(GetReportsWithReportInformations))]
        public IActionResult GetReportsWithReportInformations(Guid reportId)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "reports";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //LoadOptions.DefaultGraphicEngine = new DefaultGraphicEngine("truetype");
            using var workBook = new XLWorkbook($"{path}/report_{reportId}.xlsx");
            var workSheet = workBook.Worksheet("Rapor");
            int lastrow = workSheet.LastRowUsed().RowNumber();
            var rows = workSheet.Rows(2, lastrow);

            List<ReportDetailDto> report = new();

            foreach (IXLRow row in rows)
            {
                ReportDetailDto reportData = new();
                reportData.Location = row.Cell(1).Value.GetText();
                reportData.ContactCount = Convert.ToInt32(row.Cell(2).GetString());
                reportData.PhoneNumberCount = Convert.ToInt32(row.Cell(3).GetString());

                report.Add(reportData);
            }

            return new JsonResult(report);
        }
    }
}
