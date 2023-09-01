using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using api.Database.Entities;
using Microsoft.IdentityModel.Tokens;
using api.Interfaces;

namespace api.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public string CreateToken(UserEntity userEntity)
    {
        if (userEntity == null) throw new NullReferenceException();

        var claims = new List<Claim>()
        {
            new("firstName", userEntity.FirstName),
            new("lastName", userEntity.LastName),
            new("email", userEntity.Email),
            new("id", userEntity.Id.ToString()),
        };

        var secret = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Token"]!));
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}