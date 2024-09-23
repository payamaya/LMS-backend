namespace LMS.Models.Entities
{
    public class ActivityType
    {
        public Guid Id { get; set; }
        public string ActivityTypeName { get; set; }
        public ICollection<Activity>? Activities { get; set; }
    }
}
