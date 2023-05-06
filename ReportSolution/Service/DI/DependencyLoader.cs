using Microsoft.Extensions.DependencyInjection;
using Service.Abstract;
using Service.Concrete;

namespace Service.DI
{
    public class DependencyLoader
    {
        public static void Load(IServiceCollection services)
        {
            Repository.DI.DependencyLoader.Load(services);

            services.AddSingleton<IReportService, ReportService>();
        }
    }
}
