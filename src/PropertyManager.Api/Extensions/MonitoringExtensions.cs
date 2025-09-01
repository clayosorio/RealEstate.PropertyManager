using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace PropertyManager.Api.Extensions
{
    public static class MonitoringExtensions
    {
        public static void AddMonitoring(this IServiceCollection services, IConfiguration configuration,
        IHostBuilder host, ILoggingBuilder logging)
        {

            string serviceName = configuration["AppName"]!;

            string serviceInstanceId = Guid.NewGuid().ToString();

            host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

            logging.AddOpenTelemetry(options => options
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                        .AddService(serviceName, serviceInstanceId: serviceInstanceId)).AddConsoleExporter());


            services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                    resource.AddService(serviceName, serviceInstanceId: serviceInstanceId))
                .WithTracing(tracing => tracing
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter())
                .WithMetrics(metrics => metrics
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter());
        }
    }
}
