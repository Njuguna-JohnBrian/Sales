using api.DTOs;

namespace api.Interfaces;

public interface IAuthService
{
    Task<bool> UserExistsAsync(string email);

    Task<string> SaveUserAsync(RegistrationDto registrationDto);
}