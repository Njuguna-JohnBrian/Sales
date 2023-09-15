using api.Database.Entities;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/role")]
[Produces("application/json")]
[Authorize(Roles = "admin")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRolesAsync()
    {
        var roles = await _roleService.GetRoles();

        if (roles.Count == 0)
            return new EmptyResult();

        return Ok(
            roles.Select(rle => new
            {
                rle.RoleId,
                rle.RoleName,
                rle.RoleDescription
            })
        );
    }
}