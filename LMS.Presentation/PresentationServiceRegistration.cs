using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;
using Microsoft.OpenApi.Models;
using LMS.Presentation.Controllers;

namespace LMS.Presentation
{
    public static class PresentationServiceRegistration
    {
        public static IServiceCollection AddPresentationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers(configure =>
            {
                // Can setup formaters, the first is the default
                //options.InputFormatters.Add()
                //options.OutputFormatters.Add(formater => formater.)
                configure.ReturnHttpNotAcceptable = true;

                // Auth for all endpoints in all controllers
                //var policy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .RequireRole("User")
                //    .Build();
                //configure.Filters.Add(new AuthorizeFilter(policy));
            })
            // .AddApplicationPart(typeof(AssemblyReference).Assembly)
            //.AddApplicationPart(typeof(AutenticationController).Assembly)
            //.AddApplicationPart(typeof(CoursesController).Assembly)
            //.AddApplicationPart(typeof(DirectorsController).Assembly)
            //.AddApplicationPart(typeof(GenreController).Assembly)
            //.AddApplicationPart(typeof(ContactInformationController).Assembly)
            //.AddApplicationPart(typeof(AuthController).Assembly)
            //.AddApplicationPart(typeof(TokenController).Assembly) // Not needed but here anyway
            .AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
                opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull;
            })
            .AddNewtonsoftJson(option => option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
            //.AddXmlSerializerFormatters(); // Can return XML

            //services.AddSwaggerGen(options =>
            //{
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    options.IncludeXmlComments(xmlPath);
            //    options.EnableAnnotations();
            //});


            services.AddSwaggerGen(options =>
            {
                //// Add JWT bearer authorization to Swagger UI
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                //// Optional: Add XML comments for documentation
                //var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.EnableAnnotations();
            });

            return services;
        }
    }
}
