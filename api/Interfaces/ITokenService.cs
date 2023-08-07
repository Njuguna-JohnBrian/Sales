namespace api.Interfaces;

public interface ITokenService
{
    public string CreateToken(dynamic targetEntity, List<string> claimTarget);
    public string GetClaimValue(dynamic targetEntity, string target);
}