
namespace LMS.API;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddPersistenceServices(builder.Configuration);
		builder.Services.AddPresentationServices(builder.Configuration);
        builder.Services.AddRepositoryServices(builder.Configuration);
        builder.Services.AddInfrastructureServices();


        builder.Services.ConfigureServices();
		builder.Services.ConfigureIdentity();

        // Add services to the container.
		builder.Services.ConfigureCors();
		//builder.Services.ConfigureOpenApi();
		builder.Services.ConfigureJwt(builder.Configuration);

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		app.UseMiddleware<UnauthorizedMiddleware>();
		app.UseMiddleware<HttpExceptionHandlingMiddleware>();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
            await app.SeedDataAsync();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

		app.Run();
	}
}
