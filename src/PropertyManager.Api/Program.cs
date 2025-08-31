using PropertyManager.Api;
using PropertyManager.Api.Extensions;
using PropertyManager.Application.IoC;


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

app.Run();

public partial class Program;
