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


    public async Task<UserEntity?> UserExistsAsync(string email)
    {
        var userExists =
            await _context.UserEntities.FirstOrDefaultAsync(entity => entity.Email == email);

        return userExists;
    }

    public async Task<string> SaveUserAsync(RegistrationDto registrationDto)
    {
        var roleId = await this.GetUserRoleAsync(registrationDto?.UserRoleId);
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

        var token = _tokenService.CreateToken(user);

        return token;
    }

    public async Task<RoleEntity?> GetUserRoleAsync(Guid? roleId, string roleName = "User")
    {
        var roleQuery = _context.RoleEntities.AsQueryable();

        if (roleId.HasValue && roleId != Guid.Empty)
        {
            roleQuery = roleQuery.Where(rl => rl.RoleId == roleId);
        }

        else
        {
            roleQuery = roleQuery.Where(rl => rl.RoleName == roleName);
        }


        return await roleQuery.FirstOrDefaultAsync() ?? null;
    }
}