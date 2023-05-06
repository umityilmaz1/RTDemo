using Enums;
using Model.Base;

namespace Model.Entities
{
    public class ContactInformation : BaseGuidEntity
    {
        public ContactInformationType Type { get; set; }
        public string Content { get; set; }
        public Guid? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
