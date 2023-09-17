using Microsoft.AspNetCore.Mvc;

namespace api.tests.Systems.Services;

public class TestErrorService
{
    private readonly ErrorService _errorService = new();

    [Fact]
    public void CatchException_Returns_ObjectResult_WithDefault_ErrorMessage()
    {
        var exception = new Exception("Test Exception");

        var result = _errorService.CatchException(exception, null);

        result.Should().BeOfType<ObjectResult>();
        result.StatusCode.Should().Be(500);
        result.Value.Should().NotBeNull();
    }
    
    [Fact]
    public void CatchException_Returns_ObjectResult_With_Custom_ErrorMessage()
    {
        var exception = new Exception("Test Exception");
        var customErrorMessage = "Custom error message";

        var result = _errorService.CatchException(exception, customErrorMessage);

        result.Should().BeOfType<ObjectResult>();
        result.StatusCode.Should().Be(500);
        result.Value.Should().Be(customErrorMessage);
        
    }
}