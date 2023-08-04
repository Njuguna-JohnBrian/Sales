using api.Interfaces;
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
    public async Task<bool> RegisterUserAsync()
    {
        return await _authService.UserExistsAsync("njugunajb@gmail.com");
    }
}