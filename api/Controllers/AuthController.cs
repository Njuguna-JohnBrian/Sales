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
    private readonly PasswordService _passwordService;
    private readonly TokenService _tokenService;

    public AuthController(
        AuthService authService,
        PasswordService passwordService,
        TokenService tokenService
    )
    {
        _authService = authService;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="registrationDto"></param>
    /// <returns>Returns a token</returns>
    /// <response code="200">Returns a token</response>
    /// <response code="409">If user already exists</response>
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationDto registrationDto)
    {
        var userExists = await _authService.UserExistsAsync(registrationDto.Email);

        if (userExists != null)
        {
            return Conflict(new { message = "User exists" });
        }

        var result = await _authService.SaveUserAsync(registrationDto);

        return Ok(new { token = result });
    }

    /// <summary>
    /// Logins in a user.
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns>Returns a token</returns>
    /// <response code="200">Returns a token</response>
    /// <response code="404">If user does not exist / password is wrong</response>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginDto loginDto)
    {
        var userExists = await _authService.UserExistsAsync(loginDto.Email);

        if (userExists == null)
        {
            return NotFound(new { message = "Invalid email or password provided" });
        }


        // validate password
        var isValidPassword = _passwordService.PasswordIsValid(loginDto.Password, userExists.PasswordHash);

        if (!isValidPassword) return NotFound(new { message = "Invalid password provided" });

        var roleName = await _authService.GetUserRoleAsync(null, userExists.UserRoleId, null);

        return Ok(new { token = _tokenService.CreateToken(userExists, roleName?.RoleName) });
    }
}