namespace LMS.Models.Entities
{
    public class Module
    {
        public Guid Id { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Activity>? Activities { get; set; }
    }
}


