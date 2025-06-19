using csharp_middlewares.Services;

namespace csharp_middlewares.Middlewares
{
    public class RequestLoggingMiddleware // Conventional Middleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly RequestDelegate _next;

        // error thrown: cannot inject scoped services into the singleton.
        //private readonly ISampleScopeService _sampleScopeService;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInformation("Handling request: {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
            
            await _next(httpContext);

            _logger.LogInformation("Finished handling request. Response status code: {StatusCode}", httpContext.Response.StatusCode);
        }
    }
}
