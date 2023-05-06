using Enums;

namespace Api.Model.Contact
{
    public class ContactInformationCreateDto
    {
        public Guid ContactId { get; set; }
        public ContactInformationType Type { get; set; }
        public string Content { get; set; }
    }
}
