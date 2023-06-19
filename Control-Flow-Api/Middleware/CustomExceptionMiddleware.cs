using Control_Flow_Api.Model;
using System.Net;

namespace Control_Flow_Api.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
                await _next(httpContext);

                logger.LogInformation($"Status: {httpContext.Response.StatusCode}");
        }
    }
}
