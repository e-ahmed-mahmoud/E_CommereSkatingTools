using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred {exceptioessage}", exception.Message);

        var problemDetials = new ProblemDetails()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "an unexcepected error occured",
            Detail = exception.Message,
        };
        await httpContext.Response.WriteAsJsonAsync(problemDetials);

        return true;
    }
}
