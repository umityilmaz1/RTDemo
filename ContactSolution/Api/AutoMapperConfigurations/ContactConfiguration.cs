using Api.Model.Contact;
using AutoMapper;
using Model.Entities;

namespace Api.AutoMapperConfigurations
{
    public class ContactConfiguration : Profile
    {
        public ContactConfiguration()
        {
            CreateMap<Contact, ContactCreateDto>().ReverseMap();
            CreateMap<Contact, ContactUpdateDto>().ReverseMap();
            CreateMap<Contact, ContactListDto>().ReverseMap();
            CreateMap<Contact, ContactListWithContactInformationsDto>().ReverseMap();
        }
    }
}
