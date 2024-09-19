using LMS.Models.Entities;

namespace LMS.API
{
    public class User_NotUsed
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }

		public Guid CourseId { get; set; }
		public Course Course { get; set; }
	}
}
