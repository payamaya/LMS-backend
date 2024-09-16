namespace LMS.API
{
	public class ActivityType
	{
		public int Id { get; set; }
		public string ActivityTypeName { get; set; }
		public ICollection<Activity>? Activitys { get; set; }
	}
}
