namespace api.Helpers.Interfaces;

public interface IAuthHelperService
{
    string CreatePasswordHash(string rawPassword);

    bool PasswordIsValid(string rawPassword, string hashedPassword);
}