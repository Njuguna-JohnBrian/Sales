using api.DTOs;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="registrationDto"></param>
    /// <returns>Returns a token</returns>
    /// <response code="200">Returns a token</response>
    /// <response code="409">If user already exists</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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

    public async Task<IActionResult> LoginUserAsync([FromBody] string name)
    {
        return Ok();
    }
}