using api.Database.Entities;
using api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces;

public interface IRoleService
{
    Task<List<RoleEntity>> GetRoles();
    Task<RoleEntity> AddRole(RoleDto roleDto, HttpContext httpContext);
}