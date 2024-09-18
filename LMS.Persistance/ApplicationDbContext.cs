using LMS.Models.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistance
{
    //public class MovieCardsAPIContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    //{
    //public MovieCardsAPIContext(DbContextOptions<MovieCardsAPIContext> options)
    //    : base(options)
    //{


    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //public DbSet<User> Users => Set<User>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<Activity> Activitys => Set<Activity>();
        public DbSet<ActivityType> ActivityTypes => Set<ActivityType>();
        //public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    }
}