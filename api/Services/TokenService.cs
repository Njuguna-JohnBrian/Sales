using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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


    public string CreateToken(dynamic targetEntity, List<string> claimTarget)
    {
        if (targetEntity == null) throw new ArgumentNullException(nameof(targetEntity));


        var claims = (from target in claimTarget
            let claimValue = GetClaimValue(targetEntity, target)
            select new Claim(target, claimValue)).ToList();

        var secret = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Token"]!));
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        if (token == null)
        {
            throw new InvalidOperationException("Token generation failed");
        }

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetClaimValue(dynamic targetEntity, string target)
    {
        var property = targetEntity.GetType().GetProperty(target);
        return property?.GetValue(targetEntity)?.ToString() ?? string.Empty;
    }
}