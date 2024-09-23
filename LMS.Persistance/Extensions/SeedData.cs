using LMS.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Persistance.Extensions
{
    public static class SeedData
    {
        private static UserManager<User> userManager = null!;
        private static RoleManager<IdentityRole> roleManager = null!;
        private static IConfiguration configuration = null!;
        private const string actorRole = "Actor";
        private const string adminRole = "Admin";

        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var servicesProvider = scope.ServiceProvider;
                var db = servicesProvider.GetRequiredService<ApplicationDbContext>();

                if (await db.Courses.AnyAsync()) return;

                userManager = servicesProvider.GetRequiredService<UserManager<User>>();
                roleManager = servicesProvider.GetRequiredService<RoleManager<IdentityRole>>();
                configuration = servicesProvider.GetRequiredService<IConfiguration>();

                try
                {
                    await CreateRolesAsync(new[] { adminRole, actorRole });

                    //// Seed ActivityType

                    if (!db.ActivityTypes.Any())
                    {
                        var activityType = GenerateActivityType();
                        await db.ActivityTypes.AddRangeAsync(activityType);
                        await db.SaveChangesAsync();
                    }

                    /// //// Seed ContactInformation
                    //var contactInformation = GenerateContactInformation(5).ToList();
                    //await db.ContactInformations.AddRangeAsync(contactInformation);
                    //await db.SaveChangesAsync();

                    //// Seed Directors
                    //var directors = GenerateDirectors(5).ToList();
                    //await db.Directors.AddRangeAsync(directors);
                    //await db.SaveChangesAsync();

                    //// Seed ApplicationUsers (Actors)
                    //var actors = GenerateActors(5, contactInformation).ToList();
                    //await db.Actors.AddRangeAsync(actors);
                    //await db.SaveChangesAsync();

                    //// Seed Genres
                    //var genres = GenerateGenres(5).ToList();
                    //await db.Genres.AddRangeAsync(genres);
                    //await db.SaveChangesAsync();

                    //// Seed Movies
                    //var movies = GenerateMovies(5, directors, actors, genres).ToList();
                    //await db.Movies.AddRangeAsync(movies);
                    //await db.SaveChangesAsync();

                    //// Seed MovieActors
                    //var movieActors = GenerateMovieActors(actors, movies).ToList();
                    //await db.MovieActors.AddRangeAsync(movieActors);
                    //await db.SaveChangesAsync();

                    //// Seed MovieGenres
                    //var movieGenres = GenerateMovieGenres(genres, movies).ToList();
                    //await db.MovieGenres.AddRangeAsync(movieGenres);
                    //await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    throw;
                }
            }
        }

        private static List<ActivityType> GenerateActivityType()
        {
            return new List<ActivityType>()
           {
               new ActivityType
               {
                   ActivityTypeName = "e-learning"
               },
               new ActivityType
               {
                   ActivityTypeName = "Pluralsight"
               },
               new ActivityType
               {
                   ActivityTypeName = "Learning"
               }
           };

        }

        private static async Task CreateRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        /*    private static IEnumerable<Director> GenerateDirectors(int count)
            {
                var faker = new Faker<Director>()
                    .RuleFor(d => d.Name, f => f.Person.FullName)
                    .RuleFor(d => d.DateOfBirth, f => f.Date.Past(50, DateTime.Now.AddYears(-30)));

                return faker.Generate(count);
            }*/

        /*    private static IEnumerable<ContactInformation> GenerateContactInformation(int count)
            {
                var faker = new Faker<ContactInformation>()
                    .RuleFor(ci => ci.Email, f => f.Internet.Email())
                    .RuleFor(ci => ci.PhoneNumber, f => f.Phone.PhoneNumber());

                return faker.Generate(count);
            }*/

        /*      private static IEnumerable<ApplicationUser> GenerateActors(int count, List<ContactInformation> contactInformation)
              {
                  var faker = new Faker<ApplicationUser>()
                      .RuleFor(a => a.UserName, f => f.Internet.UserName())
                      .RuleFor(a => a.Name, f => f.Person.FullName)
                      .RuleFor(a => a.ContactInformationId, f => f.PickRandom(contactInformation).Id);

                  return faker.Generate(count);
              }*/
        /*
                private static IEnumerable<Genre> GenerateGenres(int count)
                {
                    var faker = new Faker<Genre>()
                        .RuleFor(g => g.Name, f => f.Commerce.Categories(1)[0]);

                    return faker.Generate(count);
                }*/

        /*     private static IEnumerable<MovieActor> GenerateMovieActors(List<ApplicationUser> actors, List<Movie> movies)
             {
                 var random = new Random();
                 return movies.SelectMany(movie =>
                     Enumerable.Range(0, 2).Select(_ => new MovieActor
                     {
                         ActorId = actors[random.Next(actors.Count)].Id, // Id is a string here
                         MovieId = movie.Id
                     })
                 );
             }*/

        /*      private static IEnumerable<MovieGenre> GenerateMovieGenres(List<Genre> genres, List<Movie> movies)
              {
                  var random = new Random();
                  return movies.SelectMany(movie =>
                      Enumerable.Range(0, 1).Select(_ => new MovieGenre
                      {
                          GenreId = genres[random.Next(genres.Count)].Id,
                          MovieId = movie.Id
                      })
                  );
              }*/

        /* private static IEnumerable<Movie> GenerateMovies(int count, List<Director> directors, List<ApplicationUser> actors, List<Genre> genres)
         {
             var faker = new Faker<Movie>()
                 .RuleFor(m => m.Title, f => f.Lorem.Sentence(3))
                 .RuleFor(m => m.Rating, f => f.Random.Int(1, 5))
                 .RuleFor(m => m.ReleaseDate, f => f.Date.Past(20))
                 .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
                 .RuleFor(m => m.DirectorId, f => f.PickRandom(directors).Id);

             var movies = faker.Generate(count).ToList();

             // Generate MovieActors and MovieGenres for each movie
             foreach (var movie in movies)
             {
                 movie.MovieActors = GenerateMovieActors(actors, new List<Movie> { movie }).ToList();
                 movie.MovieGenres = GenerateMovieGenres(genres, new List<Movie> { movie }).ToList();
             }

             return movies;
         }*/
    }
}
