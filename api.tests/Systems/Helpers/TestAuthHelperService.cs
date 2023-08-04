using api.Helpers.Services;

namespace api.tests.Systems.Helpers;

public class TestAuthHelperService
{
    private readonly Mock<IAuthHelperService> _mockAuthHelperService;
    private readonly AuthHelperService _authHelperService;
    private const string RawPassword = "testRawPassword";
    private const string HashedPassword = "$2a$11$LVXwqU32liNoOMql.jthMuhMgzTz/eBqTVS7xGfhXlqwmgWzyRMDC";


    public TestAuthHelperService()
    {
        _mockAuthHelperService = new Mock<IAuthHelperService>();
        _authHelperService = new AuthHelperService();
    }

    [Fact]
    public void CreatePasswordHash_Returns_A_String()
    {
        _mockAuthHelperService.Setup(service => service
                .CreatePasswordHash(RawPassword))
            .Returns(HashedPassword);

        var result = _authHelperService.CreatePasswordHash(RawPassword);

        result.Should().BeOfType<string>();
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void PasswordIsValid_Returns_A_Bool()
    {
        _mockAuthHelperService.Setup(service => service.PasswordIsValid(RawPassword, HashedPassword)).Returns(false);

        var result = _authHelperService.PasswordIsValid(RawPassword, HashedPassword);

        result.Should().BeFalse();
    }
}