using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Domain.Common.Sotrage.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;
using PropertyManager.Infrastructure.Implementations.Persistence.Sotrage;

namespace PropertyManager.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var useInMemory = configuration.GetValue<bool>("UseInMemoryDatabase", true);
            if (useInMemory)
            {
                services.AddDbContext<PropertyManagerDbContext>(o => o.UseInMemoryDatabase("PropertyManagerDb"));
            }
            else
            {
                services.AddDbContext<PropertyManagerDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddScoped<DbContext>(sp => sp.GetRequiredService<PropertyManagerDbContext>());

            services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();

            return services;
        }
    }
}
