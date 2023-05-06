using Model.Entities;
using Repository.Abstract;
using Service.Abstract;
using System.Linq.Expressions;

namespace Service.Concrete
{
    internal class ReportService : IReportService
    {
        IContactInformationRepository _contactInformationRepository;
        public ReportService(IContactInformationRepository contactInformationRepository)
        {
            _contactInformationRepository = contactInformationRepository;
        }
        public int Create(ContactInformation entity)
        {
            return _contactInformationRepository.Create(entity);
        }
        public int CreateRange(IList<ContactInformation> entities)
        {
            return _contactInformationRepository.CreateRange(entities);
        }

        public int Update(ContactInformation entity)
        {
            return _contactInformationRepository.Update(entity);
        }

        public int Delete(ContactInformation entity)
        {
            return _contactInformationRepository.Delete(entity);
        }

        public void RemoveRange(IList<ContactInformation> entities)
        {
            _contactInformationRepository.RemoveRange(entities);
        }
        
        public ContactInformation Get(Expression<Func<ContactInformation, bool>> filter)
        {
            return _contactInformationRepository.Get(filter);
        }

        public IQueryable<ContactInformation> GetList(Expression<Func<ContactInformation, bool>> filter = null)
        {
            return _contactInformationRepository.GetList(filter);
        }

        public ContactInformation GetById(Guid id)
        {
            return _contactInformationRepository.GetById(id);
        }
    }
}
