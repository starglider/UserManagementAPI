using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check for Authorization header
            StringValues token;
            if (!context.Request.Headers.TryGetValue("Authorization", out token))
            {
                Console.WriteLine("No Authorization");
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("{\"error\": \"Authorization token is missing.\"}");
                return;
            }

            // Validate the token (replace with actual validation logic)
            string tokenValue = token.ToString(); // Convert StringValues to string
            if (string.IsNullOrEmpty(tokenValue) || !IsValidToken(tokenValue))
            {
                Console.WriteLine("Authorization invalid");
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("{\"error\": \"Invalid authorization token.\"}");
                return;
            }

            // Proceed to the next middleware
            await _next(context);
        }

        private bool IsValidToken(string token)
        {
            Console.WriteLine(token);
            return token == "VALID-TOKEN";
        }
    }
}


