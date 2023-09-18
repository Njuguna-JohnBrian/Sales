using api.Database;
using api.Database.Entities;
using api.tests.Helpers;
using Microsoft.Extensions.Configuration;
using Moq.EntityFrameworkCore;

namespace api.tests.Systems.Services;

public class TestRoleService
{
    private readonly Mock<IConfiguration> _mockConfiguration = new();

    private IRoleService RoleService { get; set; }

    public TestRoleService()
    {
        Mock<DatabaseContext> mockDatabaseContext = new();
        RoleService = new RoleService(
            mockDatabaseContext.Object,
            new TokenService(_mockConfiguration.Object)
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