using Core.DataAccess.EntityFramework;
using Model.Entities;
using Repository.Abstract;
using Repository.Context;

namespace Repository.Concrete
{
    public class ContactInformationRepository : EFRepositoryBase<ContactInformation, RTDemoContext>, IContactInformationRepository
    {             
        
    }
}