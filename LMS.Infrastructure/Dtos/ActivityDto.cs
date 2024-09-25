namespace LMS.Infrastructure.Dtos
{
    public record ActivityDto
    {
        public Guid Id { get; init; }
        public string ActivityName { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; init; }
        public string Description { get; init; }

        public ActivityTypeDto ActivityType { get; init; }
    }
}
