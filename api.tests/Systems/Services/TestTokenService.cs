using System.IdentityModel.Tokens.Jwt;
using api.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace api.tests.Systems.Services;

public class TestTokenService
{
    private readonly Mock<IConfiguration> _mockConfiguration = new();
    private readonly Mock<HttpRequest> _mockHttpRequest = new();
    private readonly Mock<HttpContext> _mockHttpContext = new();
    private readonly Mock<ITokenService> _mockTokenService = new();
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
    private readonly string _jwtToken;
    private const string NullTargetClaim = "non_existent_claim";

    public TestTokenService()
    {
        _mockConfiguration.Setup(config => config[TokenConfigName])
            .Returns(TestToken);
        _tokenService = new TokenService(_mockConfiguration.Object);

        _jwtToken = _tokenService.CreateToken(TestEntity, "user");

        _mockHttpRequest.Setup(r =>
                r.Headers[HeaderNames.Authorization])
            .Returns(new Microsoft.Extensions.Primitives.StringValues("Bearer thisIsAFakeToken"));

        _mockHttpContext.SetupGet(c =>
                c.Request)
            .Returns(_mockHttpRequest.Object);
    }

    [Fact]
    public void CreateToken_Returns_A_String_And_Is_Not_Null()
    {
        _jwtToken.Should().NotBeNullOrEmpty();
        _jwtToken.Should().BeOfType<string>();
    }

    [Fact]
    public void CreateToken_Returns_A_Valid_Token()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var isValidToken = tokenHandler.CanReadToken(_jwtToken);

        isValidToken.Should().BeTrue();
    }


    [Fact]
    public void ReadToken_Returns_A_DecodedClaimValue()
    {
        var result = _tokenService.ReadToken(_jwtToken, "id");

        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ReadToken_Returns_InvalidOperationException_If_TargetIsNull()
    {
        Action result = () => _tokenService.ReadToken(_jwtToken, NullTargetClaim);

        result.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ReadToken_Returns_InvalidOperationException_If_Token_Or_TargetClaim_IsNull()
    {
        Action result = () => _tokenService.ReadToken("", "");

        result.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ParseBearerToken_Returns_A_Valid_Token()
    {
        var httpRequest = _mockHttpContext.Object.Request;

        var result = _tokenService.ParseBearerToken(httpRequest);

        result.Should<string>().BeEquivalentTo("thisIsAFakeToken");
    }

    [Fact]
    public void ParseBearerToken_Returns_InvalidOperationException_If_Token_IsNullOrEmpty()
    {
        _mockHttpRequest.Setup(r =>
                r.Headers[HeaderNames.Authorization])
            .Returns(new Microsoft.Extensions.Primitives.StringValues(""));

        var httpRequest = _mockHttpContext.Object.Request;

        Action result = () => _tokenService.ParseBearerToken(httpRequest);

        result.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void DecodeTokenFromHeaders_Return_CorrectValue()
    {
        _mockHttpRequest.Setup(r =>
                r.Headers[HeaderNames.Authorization])
            .Returns(new Microsoft.Extensions.Primitives.StringValues($"Bearer {_jwtToken}"));


        var httpRequest = _mockHttpContext.Object.Request;

        var result = _tokenService.DecodeTokenFromHeaders(httpRequest, "id");

        result.Should().BeEquivalentTo("1");
    }
}