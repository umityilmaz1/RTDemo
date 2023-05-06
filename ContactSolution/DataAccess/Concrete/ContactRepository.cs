using Core.DataAccess.EntityFramework;
using Model.Entities;
using Repository.Abstract;
using Repository.Context;

namespace Repository.Concrete
{
    internal class ContactRepository : EFRepositoryBase<Contact, RTDemoContext>, IContactRepository
    {             
        
    }
}