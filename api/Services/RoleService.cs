using api.Database;
using api.Database.Entities;
using api.DTOs;
using api.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class RoleService : IRoleService
{
    private readonly DatabaseContext _context;

    public RoleService(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }


    public async Task<List<RoleEntity>> GetRoles()
    {
        var roles = await _context.RoleEntities.ToListAsync();
        return roles;
    }

    public async Task<RoleEntity> AddRole(RoleDto roleDto)
    {
        var role = new RoleEntity
        {
            RoleName = roleDto.RoleName,
            RoleDescription = roleDto.RoleDescription
        };

        await _context.RoleEntities.AddAsync(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> RoleExists(string roleName)
    {
        var roleExists = await _context
            .RoleEntities
            .FirstOrDefaultAsync(rle => rle.RoleName == roleName);

        return (roleExists == null);
    }
}