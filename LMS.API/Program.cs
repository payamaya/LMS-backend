
namespace LMS.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddPersistenceServices(builder.Configuration);

            // Add services to the container.

    //        builder.Services.AddDbContext<ApplicationDbContext>(options =>
				//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddControllers(configure =>
				configure.ReturnHttpNotAcceptable = true
			)
				.AddApplicationPart(typeof(CoursesController).Assembly);

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
