namespace LMS.Models.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string ActivityName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }

        public Guid ModuleId { get; set; }
        public Module Module { get; set; }

        public Guid ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
