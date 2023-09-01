using api.Database.Entities;
using api.DTOs;

namespace api.Interfaces;

public interface IAuthService
{
    Task<UserEntity?> UserExistsAsync(string email);

    Task<string> SaveUserAsync(RegistrationDto registrationDto);
}