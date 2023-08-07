using System.IdentityModel.Tokens.Jwt;
using api.Database.Entities;
using Microsoft.Extensions.Configuration;

namespace api.tests.Systems.Services;

public class TestTokenService
{
    private readonly Mock<IConfiguration> _mockConfiguration = new();
    private readonly TokenService _tokenService;

    private static readonly Dictionary<string, object> TestEntity = new()
    {
        { "Id", "1" },
        { "FirstName", "testFirstName" },
        { "LastName", "testLastName" }
    };

    private static readonly List<string> ClaimTargets = new() { "Id", "FirstName", "LastName" };


    private readonly UserEntity? _nullUserEntity = null;

    private const string TestToken = "thisCannotBe21u94712-wordQHacked3242You82374can82937Try";
    private const string TokenConfigName = "Token";


    public TestTokenService()
    {
        _mockConfiguration.Setup(config => config[TokenConfigName])
            .Returns(TestToken);
        _tokenService = new TokenService(_mockConfiguration.Object);
    }

    [Fact]
    public void CreateToken_Returns_A_String_And_Is_Not_Null()
    {
        var jwtToken = _tokenService.CreateToken(TestEntity, ClaimTargets);

        jwtToken.Should().NotBeNullOrEmpty();
        jwtToken.Should().BeOfType<string>();
    }

    [Fact]
    public void CreateToken_Returns_A_Valid_Token()
    {
        var jwtToken = _tokenService.CreateToken(TestEntity, ClaimTargets);
        var tokenHandler = new JwtSecurityTokenHandler();
        var isValidToken = tokenHandler.CanReadToken(jwtToken);

        isValidToken.Should().BeTrue();
    }

    [Fact]
    public void CreateToken_NullTargetEntity_Throws_NullException()
    {
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        Action act = () => _tokenService.CreateToken(_nullUserEntity, ClaimTargets);
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.


        act.Should().Throw<ArgumentNullException>().WithParameterName("entity");
    }
}