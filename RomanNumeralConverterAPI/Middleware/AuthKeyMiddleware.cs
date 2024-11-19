using LogDBContext;
using LoggingRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RomanNumeralConverterAPI.Utilities;

namespace RomanNumeralConverterAPI.Middleware
{
    public class AuthKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _expectedKey;

        public AuthKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _expectedKey = configuration["AuthKey"]; // Load the key from configuration

        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IRepository<RMLoggingModel>>();
                if (!context.Request.Headers.TryGetValue("AuthKey", out var providedKey) || providedKey != _expectedKey)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    string result = $"Unauthorized: Missing or invalid AuthKey. Status Code 401";
                    await context.Response.WriteAsync(result);

                    await repository.AddAsync(LogEntryGenerator.GetModelFromContext(context, result));
                    return;
                }
            }

            await _next(context);
        }
    }
}
