using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Repository.Context.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder.HasOne(a => a.Contact).WithMany(a => a.ContactInformations).HasForeignKey(x => x.ContactId);
        }
    }
}