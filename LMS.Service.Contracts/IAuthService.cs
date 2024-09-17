using LMS.Models.Dtos;

using Microsoft.AspNetCore.Identity;

namespace LMS.Service.Contracts;

public interface IAuthService
{
    Task<TokenDto> CreateTokenAsync(bool expireTime);
    Task<TokenDto> RefreshTokenAsync(TokenDto token);
    Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration);
    Task<bool> ValidateUserAsync(UserForAuthenticationDto user);
}
