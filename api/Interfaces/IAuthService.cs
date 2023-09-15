using api.Database.Entities;
using api.DTOs;

namespace api.Interfaces;

public interface IAuthService
{
    Task<UserEntity?> UserExists(string email);

    Task<string> SaveUser(RegistrationDto registrationDto);

    Task<RoleEntity?> GetUserRole(Guid? roleId, long? id, string roleName);
}