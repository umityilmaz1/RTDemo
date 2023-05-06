using Api.Models;
using AutoMapper;
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

        public ReportController(ILogger<ReportController> logger, IMapper mapper, IReportService reportService)
        {
            _logger = logger;
            _mapper = mapper;
            _reportService = reportService;
        }

        //[HttpPost(nameof(RequestReport))]
        //public IActionResult RequestReport(Guid contactId)
        //{
        //    Report contact = _mapper.Map<Report>(dto);
        //    _contactService.Create(contact);
        //    return Ok();
        //}


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
            Report report = _reportService.GetById(reportId);
            ReportDetailDto detailDto = _mapper.Map<ReportDetailDto>(report);
            return new JsonResult(detailDto);
        }
    }
}
