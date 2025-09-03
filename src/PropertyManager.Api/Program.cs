using PropertyManager.Api;
using PropertyManager.Api.Extensions;
using PropertyManager.Application.IoC;
using PropertyManager.Infrastructure.Implementations.Persistence.Repositories;
using PropertyManager.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;
IHostBuilder host = builder.Host;
ILoggingBuilder logging = builder.Logging;

services
    .AddInfrastructure(configuration)
    .AddApplication()
    .AddPresentation(configuration)
    .AddMonitoring(configuration, host, logging);

var app = builder.Build();

app.UsePresentation(configuration);

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    initializer.Initialize();
}

app.Run();

public partial class Program;
