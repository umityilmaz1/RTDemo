using Enums;
using Helper;
using Model.Base;

namespace Model.Entities
{
    public class Report : BaseGuidEntity
    {
        public DateTime RequestDate { get; set; } = DateTimeHelper.NowTurkey;
        public ReportStatus status { get; set; } = ReportStatus.Preparing;
    }
}
