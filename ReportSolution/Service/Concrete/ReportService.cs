using Model.Entities;
using Repository.Abstract;
using Service.Abstract;
using System.Linq.Expressions;

namespace Service.Concrete
{
    internal class ReportService : IReportService
    {
        IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public int Create(Report entity)
        {
            return _reportRepository.Create(entity);
        }

        public int Update(Report entity)
        {
            return _reportRepository.Update(entity);
        }

        public int Delete(Report entity)
        {
            return _reportRepository.Delete(entity);
        }

        public void RemoveRange(IList<Report> entities)
        {
            _reportRepository.RemoveRange(entities);
        }
        
        public Report Get(Expression<Func<Report, bool>> filter)
        {
            return _reportRepository.Get(filter);
        }

        public IQueryable<Report> GetList(Expression<Func<Report, bool>> filter = null)
        {
            return _reportRepository.GetList(filter);
        }

        public Report GetById(Guid id)
        {
            return _reportRepository.GetById(id);
        }
    }
}
