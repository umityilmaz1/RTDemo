using Api.Model.ContactInformation;

namespace Api.Model.Contact
{
    public class ContactListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
    }
}
