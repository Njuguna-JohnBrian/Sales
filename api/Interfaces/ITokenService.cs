using api.Database.Entities;

namespace api.Interfaces;

public interface ITokenService
{
    string CreateToken(UserEntity userEntity, string? roleName);
    string DecodeTokenFromHeaders(HttpRequest httpRequest, string headersToken);
    string ParseBearerToken(HttpRequest httpRequest);
    string ReadToken(string token, string targetClaim);
}