using api.Helpers.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace api.Helpers.Services;

public class AuthHelperService : IAuthHelperService
{
    /// <summary>
    /// Creates a password hash
    /// </summary>
    /// <param name="rawPassword">The password to hash</param>
    /// <returns>The hashed password</returns>
    public string CreatePasswordHash(string rawPassword)
    {
        return BC.HashPassword(rawPassword);
    }

    /// <summary>
    /// Compares the hashed password with the un-hashed password
    /// </summary>
    /// <param name="rawPassword">un-hashed password</param>
    /// <param name="hashedPassword">hashed password</param>
    /// <returns>true if password matched or false</returns>
    public bool PasswordIsValid(string rawPassword, string hashedPassword)
    {
        return BC.Verify(rawPassword, hashedPassword);
    }
}