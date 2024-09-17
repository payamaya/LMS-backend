﻿//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity;

namespace LMS.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}