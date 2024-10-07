namespace LMS.Infrastructure.Dtos
{
    public record ActivityPostDto
    {
        public string ActivityName { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; init; }
        public string Description { get; init; }
        public Guid ActivityTypeId { get; init; }
        public Guid ModuleId { get; init; }
    }
}
