namespace csharp_middlewares.Middlewares;

public class FactoryBasedMiddleware : IMiddleware
{
    private readonly ILogger<FactoryBasedMiddleware> _logger;

    public FactoryBasedMiddleware(ILogger<FactoryBasedMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Before request");

        await next(context);

        _logger.LogInformation("After request");
    }
}
