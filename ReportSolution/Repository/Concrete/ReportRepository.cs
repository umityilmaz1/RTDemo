using Core.DataAccess.EntityFramework;
using Model.Entities;
using Repository.Abstract;
using Repository.Context;

namespace Repository.Concrete
{
    internal class ReportRepository : EFRepositoryBase<ContactInformation, RTDemoContext>, IReportRepository
    {             
        
    }
}