using LogDBContext;
using LoggingRepository.Interfaces;
using LoggingRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RomanNumeralConverterAPI.Middleware;

namespace RomanNumeralConverterAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers
            services.AddControllers();

            // Add Swagger
            services.AddEndpointsApiExplorer();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressMapClientErrors = true;
            });

            //Using In-Memory Only Because this is a demo.
            //
            services.AddDbContext<LoggingDBContext>(options =>
            options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .UseInMemoryDatabase("LoggingDB")); 

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Scoped repository

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });

                // Add the AuthKey security scheme
                c.AddSecurityDefinition("AuthKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "AuthKey",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter your API key in the field provided."
                });

                // Add the security requirement
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "AuthKey"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                                  

                });
            }
            // Apply Swagger-specific middleware
            app.UseMiddleware<SwaggerAuthMiddleware>();

            // Apply general AuthKey middleware
            app.UseMiddleware<AuthKeyMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}



