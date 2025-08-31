using PropertyManager.Domain.Common.Errors;
using System.Text.Json;

namespace PropertyManager.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (DomainError ex)
            {
                logger.LogWarning(ex, "Domain error captured");
                await WriteErrorAsync(context, ex.StatusCode, ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");
                await WriteErrorAsync(context, 500, "System.Unexpected", "An unexpected error occurred.");
            }
        }

        private static Task WriteErrorAsync(HttpContext context, int statusCode, string code, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var error = new
            {
                code,
                message,
                statusCode
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
