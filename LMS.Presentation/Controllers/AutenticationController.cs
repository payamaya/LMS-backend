//using LMS.API.Service.Contracts;
using LMS.Models.Dtos;
using LMS.Service.Contracts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Controllers;


[Route("api/authentication")]
[ApiController]
public class AutenticationController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AutenticationController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var result = await _serviceManager.AuthService.RegisterUserAsync(userForRegistration);
        return result.Succeeded ? StatusCode(StatusCodes.Status201Created) : BadRequest(result.Errors);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate(UserForAuthenticationDto user)
    {
        if (!await _serviceManager.AuthService.ValidateUserAsync(user))
            return Unauthorized();

        TokenDto tokenDto = await _serviceManager.AuthService.CreateTokenAsync(expireTime: true);
        return Ok(tokenDto);
    }
}

