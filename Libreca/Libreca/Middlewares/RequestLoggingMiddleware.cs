using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Libreca.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the request path
            _logger.LogInformation($"Request Path: {context.Request.Path}");

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
