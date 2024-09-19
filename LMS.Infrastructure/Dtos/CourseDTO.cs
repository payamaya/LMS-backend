namespace LMS.Infrastructure.Dtos
{
    public record CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        /*   public DateTime UpdatedDate { get; set; }
           public DateTime LastModifiedDate { get; set; }
           public DateTime LastModifiedBy { get; set; }
           public DateTime LastModifiedOn { get; set; }
           public DateTime LastModifiedUntil { get; set; }*/

    }
}
