using Api.Model.Contact;
using Api.Model.ContactInformation;
using AutoMapper;
using Model.Entities;

namespace Api.AutoMapperConfigurations
{
    public class ContactInformationConfiguration : Profile
    {
        public ContactInformationConfiguration()
        {
            CreateMap<ContactInformation, ContactInformationCreateDto>().ReverseMap();
            CreateMap<ContactInformation, ContactInformationListDto>().ReverseMap();
        }
    }
}
