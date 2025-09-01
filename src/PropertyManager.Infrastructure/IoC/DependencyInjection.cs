using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Infrastructure.Extension;

namespace PropertyManager.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            services.AddRepositories();
            services.AddAuthenticationInternal(configuration);
            services.AddAuthorizationInternal();

            return services;
        }
    }
}
