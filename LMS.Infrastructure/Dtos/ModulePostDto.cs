namespace LMS.Infrastructure.Dtos
{
    public record ModulePostDto
    {
        public string ModuleName { get; init; }
        public string Description { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }

        public Guid CourseId { get; init; }
       
    }
}
