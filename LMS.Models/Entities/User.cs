using Microsoft.AspNetCore.Identity;

namespace LMS.Models.Entities;

//public class ApplicationUser : IdentityUser
public class User : IdentityUser
{
    //public int Id { get; set; }
    public string Name { get; set; }
    //public string Email { get; set; }

    public int? CourseId { get; set; }
    public Course? Course { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}
