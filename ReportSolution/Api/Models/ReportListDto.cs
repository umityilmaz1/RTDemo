using Enums;

namespace Api.Models
{
    public class ReportListDto
    {
        public Guid ReportId { get; set; }
        public DateTime RequestDate { get; set; }
        public ReportStatus status { get; set; }
    }
}
