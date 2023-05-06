using Model.Entities;
using Repository.Abstract;
using Service.Abstract;
using System.Linq.Expressions;

namespace Service.Concrete
{
    internal class ContactService : IContactService
    {
        IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public int Create(Contact entity)
        {
            return _contactRepository.Create(entity);
        }
        public int CreateRange(IList<Contact> entities)
        {
            return _contactRepository.CreateRange(entities);
        }

        public int Update(Contact entity)
        {
            return _contactRepository.Update(entity);
        }

        public int Delete(Contact entity)
        {
            return _contactRepository.Delete(entity);
        }

        public void RemoveRange(IList<Contact> entities)
        {
            _contactRepository.RemoveRange(entities);
        }
        
        public Contact Get(Expression<Func<Contact, bool>> filter)
        {
            return _contactRepository.Get(filter);
        }

        public IQueryable<Contact> GetList(Expression<Func<Contact, bool>> filter = null)
        {
            return _contactRepository.GetList(filter);
        }

        public Contact GetById(Guid id)
        {
            return _contactRepository.GetById(id);
        }
    }
}
