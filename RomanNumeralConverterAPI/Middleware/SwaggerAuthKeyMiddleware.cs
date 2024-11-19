using LogDBContext;
using LoggingRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RomanNumeralConverterAPI.Utilities;

namespace RomanNumeralConverterAPI.Middleware
{
    public class SwaggerAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _expectedKey;
        public SwaggerAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _expectedKey = configuration["AuthKey"];
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IRepository<RMLoggingModel>>();
                // Use the repository here
                if (context.Request.Path.StartsWithSegments("/swagger"))
                {
                    if (!context.Request.Headers.TryGetValue("AuthKey", out var providedKey) || providedKey != _expectedKey)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        string result = $"Unauthorized: Missing or invalid AuthKey for Swagger. Status Code 401";
                        await context.Response.WriteAsync(result);

                        await repository.AddAsync(LogEntryGenerator.GetModelFromContext(context, result));
                        return;
                    }
                }
            }

   

            await _next(context);
        }
    }
}
