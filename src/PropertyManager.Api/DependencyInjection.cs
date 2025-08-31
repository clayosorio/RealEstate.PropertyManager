using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PropertyManager.Api.Extensions;
using Serilog;
using System.Reflection;

namespace PropertyManager.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
            services.AddSwaggerGenWithAuth();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddControllers();
            services.AddProblemDetails();
            services.AddEndpoints(Assembly.GetExecutingAssembly());
            services.AddCorsConfiguration(configuration);
            return services;
        }

        public static WebApplication UsePresentation(this WebApplication app, IConfiguration configuration)
        {
            app.MapEndpoints();

            app.UseCorsConfiguration(configuration);

            app.MapOpenApi();
            app.UseSwaggerWithUi();

            app.MapHealthChecks("health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseExceptionHandler();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}
