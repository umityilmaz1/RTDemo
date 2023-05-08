using Castle.Core.Configuration;
using Core.DataAccess.EntityFramework;
using Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Base;
using Model.Entities;
using Repository.Context.Configurations;
using System.Reflection.Emit;

namespace Repository.Context
{
    public class RTDemoContext : DbContext
    {
        private string _defaultConnectionString = "Default";
        private string _createDateString = "CreatedDate";
        private string _updateDateString = "UpdatedDate";

        public RTDemoContext() { }
        public RTDemoContext(DbContextOptions<RTDemoContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json")
                                                          .Build();
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString(_defaultConnectionString));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(RTDemoContext).Assembly);
            builder.ApplyConfiguration(new ContactInformationConfiguration());
            builder.ApplyConfiguration(new ContactConfiguration());
            builder.Entity<Contact>().HasQueryFilter(e => e.IsActive);
            builder.Entity<ContactInformation>().HasQueryFilter(e => e.IsActive);
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            SetCreateDateOfAddedEntries();
            SetUpdateDateOfUpdatedEntries();
            return base.SaveChanges();
        }

        private void SetCreateDateOfAddedEntries()
        {
            var addedEntries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added));

            foreach (var addedEntry in addedEntries)
            {
                addedEntry.CurrentValues[_createDateString] = DateTimeHelper.NowTurkey;
            }
        }
        private void SetUpdateDateOfUpdatedEntries()
        {
            var updatedEntries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified));

            foreach (var updatedEntry in updatedEntries)
            {
                updatedEntry.CurrentValues[_updateDateString] = DateTimeHelper.NowTurkey;
            }
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
    }
}
