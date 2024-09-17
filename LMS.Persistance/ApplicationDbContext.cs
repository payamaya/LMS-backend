using Microsoft.EntityFrameworkCore;

namespace LMS.API
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users => Set<User>();
		public DbSet<Course> Courses => Set<Course>();
		public DbSet<Module> Modules => Set<Module>();
		public DbSet<Activity> Activitys => Set<Activity>();
		public DbSet<ActivityType> ActivityTypes => Set<ActivityType>();
    }
}