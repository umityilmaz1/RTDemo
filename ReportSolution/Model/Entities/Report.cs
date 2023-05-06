using Enums;
using Model.Base;

namespace Model.Entities
{
    public class Report : BaseGuidEntity
    {
        public Guid RequesterId { get; set; }
        public DateTime RequestDate { get; set; }
        public ReportStatus status { get; set; }
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
