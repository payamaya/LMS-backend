namespace LMS.Infrastructure.Dtos
{
    public record ModuleDto
    {
        public Guid Id { get; init; }
        public string ModuleName { get; init; }
        public string Description { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public string State { get; init; }

        public ICollection<ActivityDto>? Activities { get; init; }
    }
}
