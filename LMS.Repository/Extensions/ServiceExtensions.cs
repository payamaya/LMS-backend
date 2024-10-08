﻿using LMS.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Repository.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped(
                provider => new Lazy<ICourseRepository>(
                    () => provider.GetRequiredService<ICourseRepository>()
                )
            );

            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped(
                provider => new Lazy<IActivityRepository>(
                    () => provider.GetRequiredService<IActivityRepository>()
                )
            );

            services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddScoped(
                provider => new Lazy<IActivityTypeRepository>(
                    () => provider.GetRequiredService<IActivityTypeRepository>()
                )
            );

            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped(
                provider => new Lazy<IModuleRepository>(
                    () => provider.GetRequiredService<IModuleRepository>()
                )
            );

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(
                provider => new Lazy<IUserRepository>(
                    () => provider.GetRequiredService<IUserRepository>()
                )
            );
            /*
              services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IContactInfoRepository, ContactInfoRepository>();*/

            /*      
                   services.AddScoped(
                       provider => new Lazy<ICourseRepository>(
                           () => provider.GetRequiredService<ICourseRepository>()
                       )
                   );
                   services.AddScoped(
                       provider => new Lazy<IActorRepository>(
                           () => provider.GetRequiredService<IActorRepository>()
                       )
                   );
                   services.AddScoped(
                       provider => new Lazy<IDirectorRepository>(
                           () => provider.GetRequiredService<IDirectorRepository>()
                       )
                   );
                   services.AddScoped(
                       provider => new Lazy<IGenreRepository>(
                           () => provider.GetRequiredService<IGenreRepository>()
                       )
                   );
                   services.AddScoped(
                       provider => new Lazy<IContactInfoRepository>(
                           () => provider.GetRequiredService<IContactInfoRepository>()
                       )
                   );*/
        }
    }
}