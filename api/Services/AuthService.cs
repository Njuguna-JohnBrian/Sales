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

    public AuthService(DatabaseContext databaseContext, PasswordService passwordService, IConfiguration config)
    {
        _context = databaseContext;
        _passwordService = passwordService;
        _tokenService = new TokenService(config);
    }


    public async Task<bool> UserExistsAsync(string email)
    {
        var userExists =
            await _context.UserEntities.FirstOrDefaultAsync(entity => entity.Email == email);

        return userExists != null;
    }

    public async Task<string> SaveUserAsync(RegistrationDto registrationDto)
    {
        var user = new UserEntity
        {
            FirstName = registrationDto.FirstName,
            LastName = registrationDto.LastName,
            Email = registrationDto.Email,
            PasswordHash = _passwordService.CreatePasswordHash(registrationDto.Password),
            RegistrationDTM = DateTime.Now,
        };

        await _context.UserEntities.AddAsync(user);

        await _context.SaveChangesAsync();

        var token = _tokenService.CreateToken(user, new List<string> { user.FirstName, user.LastName, user.Email });
        return token;
    }
}