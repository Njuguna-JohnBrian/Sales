﻿namespace api.tests.Systems.Helpers;

public class TestAuthService
{
    private readonly Mock<IPasswordService> _mockPasswordService;
    private readonly PasswordService _passwordService;
    private const string RawPassword = "testRawPassword";
    private const string HashedPassword = "$2a$11$LVXwqU32liNoOMql.jthMuhMgzTz/eBqTVS7xGfhXlqwmgWzyRMDC";


    public TestAuthService()
    {
        _mockPasswordService = new Mock<IPasswordService>();
        _passwordService = new PasswordService();
    }


    [Fact]
    public void CreatePasswordHash_Returns_A_String()
    {
        _mockPasswordService.Setup(service => service
                .CreatePasswordHash(RawPassword))
            .Returns(HashedPassword);

        var result = _passwordService.CreatePasswordHash(RawPassword);

        result.Should().BeOfType<string>();
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void PasswordIsValid_Returns_A_Bool()
    {
        _mockPasswordService.Setup(service => service.PasswordIsValid(RawPassword, HashedPassword)).Returns(false);

        var result = _passwordService.PasswordIsValid(RawPassword, HashedPassword);

        result.Should().BeFalse();
    }
}