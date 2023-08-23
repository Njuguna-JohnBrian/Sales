using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces;

public interface IErrorService
{
    ObjectResult CatchException(Exception exception, string? errorMessage);
}