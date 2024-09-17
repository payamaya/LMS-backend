using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos;
public record UserForAuthenticationDto
{
    [Required]
    public string? UserName { get; init; }

    [Required]
    public string? Password { get; init; }
}
