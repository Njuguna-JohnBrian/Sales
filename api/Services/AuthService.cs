using api.Database;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class AuthService : IAuthService

{
    private readonly DatabaseContext _context;

    public AuthService(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        var userExists =
            await _context.UserEntities.FirstOrDefaultAsync(entity => entity.Email == email);

        return userExists != null;
    }
}