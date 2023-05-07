using Enums;

namespace Api.Models
{
    public class ReportListDto
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public ReportStatus Status { get; set; }
    }
}
