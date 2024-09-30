using Bogus;

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
		private static UserManager<User> _userManager = null!;
		private static RoleManager<IdentityRole> _roleManager = null!;
		private static IConfiguration _configuration = null!;
		private const string _studentRole = "Student";
		private const string _teacherRole = "Teacher";

		public static async Task SeedDataAsync(this IApplicationBuilder builder)
		{
			using (var scope = builder.ApplicationServices.CreateScope())
			{
				var servicesProvider = scope.ServiceProvider;
				var db = servicesProvider.GetRequiredService<ApplicationDbContext>();

				if (await db.Courses.AnyAsync()) return;

				_userManager = servicesProvider.GetRequiredService<UserManager<User>>()
					?? throw new Exception("UserManager not exits in DI");
				_roleManager = servicesProvider.GetRequiredService<RoleManager<IdentityRole>>()
					?? throw new Exception("RoleManager not exits in DI");
				_configuration = servicesProvider.GetRequiredService<IConfiguration>()
					?? throw new Exception("Configuration not exits in DI");

				try
				{

					//// Seed ActivityType

					if (!db.Activitys.Any())
					{
						var activityTypes = GenerateActivityType();
						await db.ActivityTypes.AddRangeAsync(activityTypes);
						//await db.SaveChangesAsync();

						var courses = GenerateCourses();
						await db.Courses.AddRangeAsync(courses);
						//await db.SaveChangesAsync();

						var modules = GenerateModules(courses);
						await db.Modules.AddRangeAsync(modules);
						//await db.SaveChangesAsync();

						var activities = GenerateActivities(modules, activityTypes);
						await db.Activitys.AddRangeAsync(activities);

						await CreateRolesAsync(new[] { _teacherRole, _studentRole });

						await db.SaveChangesAsync();

						var users = await GenerateUsersForCourse(
							courses,
							//teachers[i],
							count: 12);
						//await db.Users.AddRangeAsync(users);
					}

					//DateTime.Parse("2024-03-24");
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

		private static List<Activity> GenerateActivities(List<Module> modules, List<ActivityType> activityTypes)
		{
			return new List<Activity>
			{
				new Activity
				{
					ActivityName = "Lecture 1: Variables and Data Types",
					Description = "Introduction to basic data types in Python",
					StartTime = DateTime.Parse("2023-01-05T12:00:00"),
					EndTime = DateTime.Parse("2023-01-05T14:00:00"),
					Module = modules[0],
					ActivityType = activityTypes[0]
				},
				new Activity
				{
					ActivityName = "Assignment 1: Basic Python Program",
					Description = "Create a simple Python program.",
					StartTime = DateTime.Parse("2023-01-07T12:00:00"),
					EndTime = DateTime.Parse("2023-01-14T00:00:00"),
					Module = modules[0],
					ActivityType = activityTypes[1]
				},
				new Activity
				{
					ActivityName = "Lecture 1: Arrays and Linked Lists",
					Description = "Overview of arrays and linked lists.",
					StartTime = DateTime.Parse("2023-02-20T09:00:00"),
					EndTime = DateTime.Parse("2023-02-21T00:00:00"),
					Module = modules[1],
					ActivityType = activityTypes[0]
				},
				new Activity
				{
					ActivityName = "Assignment 1: Implement a Linked List",
					Description = "Implement a simple linked list in Python.",
					StartTime = DateTime.Parse("2023-02-22T10:00:00"),
					EndTime = DateTime.Parse("2023-03-01T00:00:00"),
					Module = modules[1],
					ActivityType = activityTypes[2]
				},
				new Activity
				{
					ActivityName = "Lecture 2: Variables and Data Types",
					Description = "Introduction to basic data types in Python",
					StartTime = DateTime.Parse("2023-01-05T09:00:00"),
					EndTime = DateTime.Parse("2023-01-05T11:30:00"),
					Module = modules[2],
					ActivityType = activityTypes[0]
				},
				new Activity
				{
					ActivityName = "Assignment 2: Basic Python Program",
					Description = "Create a simple Python program.",
					StartTime = DateTime.Parse("2023-01-07T12:00:00"),
					EndTime = DateTime.Parse("2023-01-14T00:00:00"),
					Module = modules[2],
					ActivityType = activityTypes[1]
				},
				new Activity
				{
					ActivityName = "Lecture 2: Arrays and Linked Lists",
					Description = "Overview of arrays and linked lists.",
					StartTime = DateTime.Parse("2023-02-20T13:00:00"),
					EndTime = DateTime.Parse("2023-02-20T15:00:00"),
					Module = modules[3],
					ActivityType = activityTypes[0]
				},
				new Activity
				{
					ActivityName = "Assignment 2: Implement a Linked List",
					Description = "Implement a simple linked list in Python.",
					StartTime = DateTime.Parse("2023-02-22T12:00:00"),
					EndTime = DateTime.Parse("2023-03-01T00:00:00"),
					Module = modules[3],
					ActivityType = activityTypes[2]
				},
				new Activity
				{
					ActivityName = "Lecture 2: Arrays and Linked Lists",
					Description = "Overview of arrays and linked lists.",
					StartTime = DateTime.Parse("2023-02-20T13:00:00"),
					EndTime = DateTime.Parse("2023-02-20T15:00:00"),
					Module = modules[4],
					ActivityType = activityTypes[0]
				},
				new Activity
				{
					ActivityName = "Assignment 2: Implement a Linked List",
					Description = "Implement a simple linked list in Python.",
					StartTime = DateTime.Parse("2023-02-22T12:00:00"),
					EndTime = DateTime.Parse("2023-03-01T00:00:00"),
					Module = modules[4],
					ActivityType = activityTypes[2]
				},
				new Activity
				{
					ActivityName = "Lecture 2: Arrays and Linked Lists",
					Description = "Overview of arrays and linked lists.",
					StartTime = DateTime.Parse("2023-02-20T13:00:00"),
					EndTime = DateTime.Parse("2023-02-20T15:00:00"),
					Module = modules[5],
					ActivityType = activityTypes[0]
				},
				new Activity
				{
					ActivityName = "Assignment 2: Implement a Linked List",
					Description = "Implement a simple linked list in Python.",
					StartTime = DateTime.Parse("2023-02-22T12:00:00"),
					EndTime = DateTime.Parse("2023-03-01T00:00:00"),
					Module = modules[5],
					ActivityType = activityTypes[2]
				}
			};
		}

		private static List<Module> GenerateModules(List<Course> courses)
		{
			return new List<Module>
			{
				new Module
				{
					ModuleName = "Introduction to Programming",
					Description = "Learn the basics of programming using Python.",
					StartDate = DateTime.Parse("2024-01-05"),
					EndDate = DateTime.Parse("2024-05-15"),
					Course = courses[0]
				},
				new Module
				{
					ModuleName = "Data Structures",
					Description = "An introduction to common data structures.",
					StartDate = DateTime.Parse("2024-06-10"),
					EndDate = DateTime.Parse("2024-11-10"),
					Course = courses[0]
				},
				new Module
				{
					ModuleName = "AI Engineering",
					Description = "Intro to AI",
					StartDate = DateTime.Parse("2024-11-15"),
					EndDate = DateTime.Parse("2024-12-31"),
					Course = courses[0]
				},
				new Module
				{
					ModuleName = "Syntax school",
					Description = "What is syntax?",
					StartDate = DateTime.Parse("2024-02-05"),
					EndDate = DateTime.Parse("2024-05-25"),
					Course = courses[1]
				},
				new Module
				{
					ModuleName = "Introduction to Webdevelopment",
					Description = "Learn the basics of programming using HTML and CSS.",
					StartDate = DateTime.Parse("2024-08-05"),
					EndDate = DateTime.Parse("2024-10-15"),
					Course = courses[1]
				},
				new Module
				{
					ModuleName = "Introduction to JavaScript",
					Description = "An introduction to basic JavaScript.",
					StartDate = DateTime.Parse("2024-11-20"),
					EndDate = DateTime.Parse("2024-12-10"),
					Course = courses[1]
				}
			};
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

		private static List<Course> GenerateCourses()
		{
			return new List<Course>()
		   {
			   new Course
			   {
				   CourseName = "Computer Science Basics",
				   Description = "Introduction to computer science principles.",
				   StartDate = DateTime.Parse("2023-01-01")
			   },
			   new Course
			   {
				   CourseName = "Web Development",
				   Description = "Introduction to Web development.",
				   StartDate = DateTime.Parse("2023-01-01")
			   }
		   };

		}

		private static async Task CreateRolesAsync(string[] roleNames)
		{
			foreach (var roleName in roleNames)
			{
				if (await _roleManager.RoleExistsAsync(roleName)) continue;

				var role = new IdentityRole { Name = roleName };
				var result = await _roleManager.CreateAsync(role);
				if (!result.Succeeded)
					throw new Exception(string.Join("\n", result.Errors));
			}
		}

		private async static Task<IEnumerable<User>> GenerateUsersForCourse(
			List<Course> courses,
			int count)
		{
			var users = GetUsers(courses[0], count);
			users[0].IsStudent = false;
			users[0].UserName = "teacher";

			var users1 = GetUsers(courses[1], count);
			users1[0].IsStudent = false;
			users1[1].UserName = "student";

			users.AddRange(users1);

			var password = _configuration["password"]
				?? throw new Exception("password not exist in config"); // From secret

			foreach (var user in users)
			{
				var result = await _userManager.CreateAsync(user, password);
				if (!result.Succeeded)
					throw new Exception(string.Join("\n", result.Errors));

				await _userManager.AddToRoleAsync(user, user.IsStudent
					? _studentRole : _teacherRole);
			}

			return users;
		}

		private static List<User> GetUsers(Course course, int count)
		{
			var faker = new Faker<User>()
				.RuleFor(u => u.UserName, f => f.Person.UserName)
				.RuleFor(u => u.Name, f => f.Person.FullName)
				.RuleFor(u => u.Email, f => f.Internet.Email())
				.RuleFor(u => u.IsStudent, true)
				.RuleFor(u => u.Course, course);

			var users = new List<User>();
			users.AddRange(faker.Generate(count));

			return users;
		}

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
