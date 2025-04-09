using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log HTTP method and request path
            var method = context.Request.Method;
            var path = context.Request.Path;

            await _next(context);

            // Log response status code
            var statusCode = context.Response.StatusCode;

            Console.WriteLine($"HTTP {method} {path} responded with status code {statusCode}");
        }
    }
}
