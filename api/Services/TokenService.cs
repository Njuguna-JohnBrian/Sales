using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
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

    public string CreateToken<TEntity>(TEntity entity, List<string> claimTarget) where TEntity : class
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));


        var claims = (from target in claimTarget let claimValue = GetClaimValue(entity, target) select new Claim(target, claimValue)).ToList();

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

    public string GetClaimValue<TEntity>(TEntity entity, string target) where TEntity : class
    {
        var property = typeof(TEntity).GetProperty(target,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null) return string.Empty;
        var propertyValue = property.GetValue(entity);
        return (propertyValue != null ? propertyValue.ToString() : string.Empty)!;
    }
}