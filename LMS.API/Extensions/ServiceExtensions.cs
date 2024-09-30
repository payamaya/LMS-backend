

namespace LMS.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddPolicy("AllowAll", p =>
                p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

        });
    }

    public static void ConfigureOpenApi(this IServiceCollection services) =>
        services.AddEndpointsApiExplorer()
                .AddSwaggerGen(setup =>
                {
                    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Place to add JWT with Bearer",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    });

                    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });
                });


    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped(provider => new Lazy<IAuthService>(() => provider.GetRequiredService<IAuthService>())); 

        services.AddScoped<IUserService, UserService>();
        services.AddScoped(provider => new Lazy<IUserService>(() => provider.GetRequiredService<IUserService>()));

        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped(provider => new Lazy<IModuleService>(() => provider.GetRequiredService<IModuleService>()));

        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped(provider => new Lazy<ICourseService>(() => provider.GetRequiredService<ICourseService>()));

        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped(provider => new Lazy<IActivityService>(() => provider.GetRequiredService<IActivityService>()));

        services.AddScoped<IActivityTypeService, ActivityTypeService>();
        services.AddScoped(provider => new Lazy<IActivityTypeService>(() => provider.GetRequiredService<IActivityTypeService>()));
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        //ToDo: Set in "Manage User Secrets" (right click project and you find it!). Example "secretkey" : "ReallyLongKeyItDosentWorkIfItsNotAtleat32Caracters!!!!!!!!!!!!!!!!!!!!!!!!"
        var secretkey = configuration["secretkey"];
        ArgumentNullException.ThrowIfNull(secretkey, nameof(secretkey));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            ArgumentNullException.ThrowIfNull(jwtSettings, nameof(jwtSettings));

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
            };

        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        // Lägg till dataskyddstjänsterna för att möjliggöra tokenprovider
        services.AddDataProtection();

        services.AddIdentityCore<User>(opt =>
        {
            // Set change password, rules for password
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 3;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}
