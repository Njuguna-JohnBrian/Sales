namespace api.Interfaces;

public interface ITokenService
{
    public string CreateToken<TEntity>(TEntity entity, List<string> claimTarget) where TEntity : class;
    public string GetClaimValue<TEntity>(TEntity entity, string target) where TEntity : class;
}