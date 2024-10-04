namespace LMS.Infrastructure.Dtos
{
    public class CoursePostDto
    {
        public string CourseName { get; init; }
        public string Description { get; init; }
        public DateTime StartDate { get; init; }
        public Guid? TeacherId { get; init; }
    }
}
