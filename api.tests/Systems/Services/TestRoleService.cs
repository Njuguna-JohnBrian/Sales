using api.Database;
using api.Database.Entities;
using api.tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Moq.EntityFrameworkCore;

namespace api.tests.Systems.Services;

public class TestRoleService
{
    private readonly Mock<IConfiguration> _mockConfiguration = new();
    private readonly TokenService _tokenService;
    private readonly string _jwtToken;
    private IRoleService RoleService { get; }
    private readonly Mock<HttpRequest> _mockHttpRequest = new();
    private readonly Mock<HttpContext> _mockHttpContext = new();
    private const string TokenConfigName = "Token";
    private const string TestToken = "thisCannotBe21u94712-wordQHacked3242You82374can82937Try";

    private static readonly UserEntity TestEntity = new()
    {
        Id = 1,
        FirstName = "testUser",
        LastName = "testUser",
        Email = "test@email.com",
        PasswordHash = "$2a$11$8STxovb1zSpiMqGkxOcJbOkKvvUTYWWEE2dW92mmpZ1JvPhDUJwjO"
    };

    public TestRoleService()
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
        Mock<DatabaseContext> mockDatabaseContext = new();
        RoleService = new RoleService(
            mockDatabaseContext.Object,
            _tokenService
        );

        mockDatabaseContext.Setup(x => x.RoleEntities)
            .ReturnsDbSet(
                TestDataHelper.GetFakeRolesList()
            );
    }

    [Fact]
    public async void GetRoles_Returns_All_Roles()
    {
        var result = await RoleService.GetRoles();
        result.Should().NotBeNull();
        result.Should().BeOfType<List<RoleEntity>>();
    }

    [Fact]
    public async void RoleExists_Returns_A_Role()
    {
        var result = await RoleService
            .RoleExists(
                TestDataHelper.GetFakeRolesList().First().RoleName
            );
        result.Should().NotBeNull();
        result.Should().BeOfType<RoleEntity>();
    }
}