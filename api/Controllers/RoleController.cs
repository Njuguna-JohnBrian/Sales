using api.DTOs;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/role")]
[Produces("application/json")]
[Authorize(Roles = "admin")]
[AllowAnonymous]
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

    [HttpPost]
    public async Task<IActionResult> AddRoleAsync([FromBody] RoleDto roleDto)
    {
        var roleExists = await _roleService.RoleExists(roleDto.RoleName);
        if (roleExists != null)
            return Conflict("A role with that name exists");
        await _roleService.AddRole(roleDto, HttpContext);
        return Ok("Role created");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleDto updateRoleDto)
    {
        var roleExists = await _roleService.RoleExists(updateRoleDto.RoleName);
        if (roleExists == null)
            return Conflict("A role with that doesn't exist");
        await _roleService.UpdateRole(updateRoleDto, roleExists, HttpContext);

        return Ok($"Role {updateRoleDto.RoleName} updated");
    }
}