using api.Database;
using api.Database.Entities;
using api.DTOs;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class RoleService : IRoleService
{
    private readonly DatabaseContext _context;
    private readonly TokenService _tokenService;

    public RoleService(DatabaseContext databaseContext,
        TokenService tokenService
    )
    {
        _context = databaseContext;
        _tokenService = tokenService;
    }


    public async Task<List<RoleEntity>> GetRoles()
    {
        var roles = await _context.RoleEntities.ToListAsync();
        return roles;
    }

    public async Task<RoleEntity> AddRole(RoleDto roleDto, HttpContext httpContext)
    {
        var role = new RoleEntity
        {
            RoleName = roleDto.RoleName,
            RoleDescription = roleDto.RoleDescription,
            CreatedDtm = DateTime.Now,
            CreatedBy = Convert.ToInt64(_tokenService
                .DecodeTokenFromHeaders(httpContext.Request,
                    "id")
            )
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