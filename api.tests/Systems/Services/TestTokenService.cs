using System.IdentityModel.Tokens.Jwt;
using api.Database.Entities;
using Microsoft.Extensions.Configuration;

namespace api.tests.Systems.Services;

public class TestTokenService
{
    private readonly Mock<IConfiguration> _mockConfiguration = new();
    private readonly TokenService _tokenService;

    private static readonly UserEntity TestEntity = new()
    {
        Id = 1,
        FirstName = "testUser",
        LastName = "testUser",
        Email = "test@email.com",
        PasswordHash = "$2a$11$8STxovb1zSpiMqGkxOcJbOkKvvUTYWWEE2dW92mmpZ1JvPhDUJwjO"
    };

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
        var jwtToken = _tokenService.CreateToken(TestEntity);

        jwtToken.Should().NotBeNullOrEmpty();
        jwtToken.Should().BeOfType<string>();
    }

    [Fact]
    public void CreateToken_Returns_A_Valid_Token()
    {
        var jwtToken = _tokenService.CreateToken(TestEntity);
        var tokenHandler = new JwtSecurityTokenHandler();
        var isValidToken = tokenHandler.CanReadToken(jwtToken);

        isValidToken.Should().BeTrue();
    }
}