using Model.Base;

namespace Model.Entities
{
    public class Contact : BaseGuidEntity
    {
        public Contact()
        {
            ContactInformations = new HashSet<ContactInformation>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public virtual HashSet<ContactInformation> ContactInformations { get; set; }
    }
}
