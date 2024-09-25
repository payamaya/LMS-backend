using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;

namespace LMS.Models.Entities;

//public class ApplicationUser : IdentityUser
public class User : IdentityUser
{
    //public int Id { get; set; }
    public string Name { get; set; }
    //public string Email { get; set; }

    public bool IsStudent { get; set; }

    public Guid? CourseId { get; set; }
    public Course? Course { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}
