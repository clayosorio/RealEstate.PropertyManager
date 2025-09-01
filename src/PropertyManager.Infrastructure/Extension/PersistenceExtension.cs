using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Infrastructure.Extension
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
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

            var connectionString = configuration.GetConnectionString("AzureBlobStorage");
            services.AddSingleton(new BlobServiceClient(connectionString));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<PropertyManagerDbContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
