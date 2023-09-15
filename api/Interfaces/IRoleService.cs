using api.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces;

public interface IRoleService
{
    Task<List<RoleEntity>> GetRolesAsync();
}