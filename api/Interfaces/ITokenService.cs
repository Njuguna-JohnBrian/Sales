using api.Database.Entities;

namespace api.Interfaces;

public interface ITokenService
{
    public string CreateToken(UserEntity userEntity);
}