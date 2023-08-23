using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Services;



public class ErrorService : ControllerBase, IErrorService
{
    public ObjectResult CatchException(Exception ex, string? errorMessage)
    {
        System.Diagnostics.Trace.TraceError(ex.Message);
        System.Diagnostics.Trace.TraceError(ex.StackTrace);
        System.Diagnostics.Trace.TraceError(ex.InnerException?.Message);
        return StatusCode(500, errorMessage ?? "Can not process request at this time. Try again later.");
    }
}