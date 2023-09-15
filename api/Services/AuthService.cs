using api.Database;
using api.Database.Entities;
using api.DTOs;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace api.Services;

public class AuthService : IAuthService

{
    private readonly DatabaseContext _context;
    private readonly PasswordService _passwordService;
    private readonly TokenService _tokenService;

    public AuthService(
        DatabaseContext databaseContext,
        PasswordService passwordService,
        IConfiguration config
    )
    {
        _context = databaseContext;
        _passwordService = passwordService;
        _tokenService = new TokenService(config);
    }


    public async Task<UserEntity?> UserExists(string email)
    {
        var userExists =
            await _context.UserEntities.FirstOrDefaultAsync(entity => entity.Email == email);

        return userExists;
    }

    public async Task<string> SaveUser(RegistrationDto registrationDto)
    {
        var roleId = await GetUserRole(registrationDto.UserRoleId ?? null, null);
        var user = new UserEntity
        {
            FirstName = registrationDto.FirstName,
            LastName = registrationDto.LastName,
            Email = registrationDto.Email,
            PasswordHash = _passwordService.CreatePasswordHash(registrationDto.Password),
            UserRoleId = roleId?.Id,
            RegistrationDtm = DateTime.Now,
        };

        await _context.UserEntities.AddAsync(user);

        await _context.SaveChangesAsync();

        var token = _tokenService.CreateToken(user, roleId?.RoleName);

        return token;
    }


    public async Task<RoleEntity?> GetUserRole(Guid? roleId, long? id, string roleName = "User")
    {
        var roleQuery = _context.RoleEntities.AsQueryable();

        if (roleId.HasValue && roleId != Guid.Empty)
        {
            roleQuery = roleQuery.Where(rl => rl.RoleId == roleId);
        }
        else if (id.HasValue && id != 0)
        {
            roleQuery = roleQuery.Where(rl => rl.Id == id);
        }

        else
        {
            roleQuery = roleQuery.Where(rl => rl.RoleName == roleName);
        }


        return await roleQuery.FirstOrDefaultAsync() ?? null;
    }
}