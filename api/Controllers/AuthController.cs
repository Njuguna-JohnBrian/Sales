using api.DTOs;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationDto registrationDto)
    {
        var userExists = await _authService.UserExistsAsync(registrationDto.Email);

        if (userExists)
        {
            return Conflict(new { message = "User exists" });
        }

        var result = await _authService.SaveUserAsync(registrationDto);

        return Ok(new { token = result });
    }
}