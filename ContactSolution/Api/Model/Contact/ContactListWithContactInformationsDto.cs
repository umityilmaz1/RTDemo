using Api.Model.ContactInformation;

namespace Api.Model.Contact
{
    public class ContactListWithContactInformationsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public List<ContactInformationListDto> ContactInformations { get; set; }
    }
}
