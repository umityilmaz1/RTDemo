using Microsoft.Extensions.DependencyInjection;
using Repository.Abstract;
using Repository.Concrete;

namespace Repository.DI
{
    public class DependencyLoader
    {
        public static void Load(IServiceCollection services) 
        {
            services.AddSingleton<IContactRepository, ContactRepository>();
            services.AddSingleton<IContactInformationRepository, ContactInformationRepository>();
        }
    }
}
