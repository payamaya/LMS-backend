namespace LMS.Infrastructure.Dtos
{
    public record CourseDto
    {
        public Guid Id { get; init; }
        public string CourseName { get; init; }
        public string Description { get; init; }
        public DateTime StartDate { get; init; }
        public UserDto? Teacher { get; init; }
    }
}
