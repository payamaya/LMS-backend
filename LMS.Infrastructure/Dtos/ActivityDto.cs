namespace LMS.Infrastructure.Dtos
{
    public record ActivityDto
    {
        public Guid Id { get; init; }
        public string ActivityName { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; init; }
        public string Description { get; init; }

        public Guid ModuleId { get; init; }
        public ModuleDto Module { get; init; }

        public Guid ActivityTypeId { get; init; }
        public ActivityTypeDto ActivityType { get; init; }
    }
}
