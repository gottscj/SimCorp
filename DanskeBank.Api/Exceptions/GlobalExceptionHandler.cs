using Microsoft.AspNetCore.Diagnostics;

namespace DanskeBank.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not BadRequestException)
        {
            return false;
        }
        
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        var exceptionMessage = exception.Message;
        _logger.LogError(
            "Error Message: {ExceptionMessage}, Time of occurrence {Time}",
            exceptionMessage, DateTime.UtcNow);
        await httpContext.Response.WriteAsync(exception.Message, cancellationToken: cancellationToken);
        return true;
    }
}