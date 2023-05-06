using Enums;

namespace Api.Model.ContactInformation
{
    public class ContactInformationListDto
    {
        public Guid Id { get; set; }
        public ContactInformationType Type { get; set; }
        public string Content { get; set; }
    }
}
