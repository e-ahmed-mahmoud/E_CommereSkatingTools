namespace API.Middleware;

public class HttpInfoLogMiddleware(ILogger<HttpInfoLogMiddleware> logger, RequestDelegate next)
{
    private readonly ILogger<HttpInfoLogMiddleware> _logger = logger;
    private readonly RequestDelegate _next = next;

    public Task InvokeAsync(HttpContext httpContext)
    {
        _logger.LogInformation("Source {Source} path {path}", httpContext.Connection.RemoteIpAddress?.ToString(), httpContext.Request.Path);
        return _next(httpContext);
    }
}

public static class HttpInfoLog
{
    public static IApplicationBuilder UseHttpInfoLog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpInfoLogMiddleware>();
    }
}
