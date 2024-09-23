using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using LMS.Persistance.Extensions;

namespace LMS.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureSql(configuration);

            return services;
        }
    }
}
