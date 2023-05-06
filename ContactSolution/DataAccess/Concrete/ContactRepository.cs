using Core.DataAccess.EntityFramework;
using Model.Entities;
using Repository.Abstract;
using Repository.Context;

namespace Repository.Concrete
{
    public class ContactRepository : EFRepositoryBase<Contact, RTDemoContext>, IContactRepository
    {             
        
    }
}