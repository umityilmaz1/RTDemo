using Enums;

namespace Model.Entities
{
    public class ContactInformation
    {
        public ContactInformationType Type { get; set; }
        public string Content { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
