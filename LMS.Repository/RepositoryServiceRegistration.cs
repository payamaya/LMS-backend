using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using LMS.Repository.Extensions;

namespace LMS.Repository
{
    public static class RepositoryServiceRegistration
    {
        public static IServiceCollection AddPresentationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureRepositories();

            return services;
        }
    }
}
