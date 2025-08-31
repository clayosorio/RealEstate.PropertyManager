using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Domain.Common.Sotrage.Repositories;
using PropertyManager.Domain.Properties.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;
using PropertyManager.Infrastructure.Implementations.Persistence.Repositories;
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

            var connectionString = configuration.GetConnectionString("AzureBlobStorage");
            services.AddSingleton(new BlobServiceClient(connectionString));
            services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            return services;
        }
    }
}
