using LMS.Infrastructure.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CourseMapperProfile));
        }
    }
}