using LMS.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services)
        {
            services.ConfigureAutoMapper();

            return services;
        }
    }
}
