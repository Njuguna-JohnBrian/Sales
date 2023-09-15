using api.Database;
using api.Database.Entities;
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


    public async Task<List<RoleEntity>> GetRolesAsync()
    {
        var roles = await _context.RoleEntities.ToListAsync();
        return roles;
    }
}