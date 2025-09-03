using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication;
using PropertyManager.Domain.Abstractions.Repositories;
using PropertyManager.Domain.Owners.Repositories;
using PropertyManager.Domain.Properties.Repositories;
using PropertyManager.Domain.PropertyImages.Repositories;
using PropertyManager.Infrastructure.Implementations.Configurations;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;
using PropertyManager.Infrastructure.Implementations.Persistence.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.Sotrage;
using PropertyManager.Infrastructure.Implementations.Security.Authentication;

namespace PropertyManager.Infrastructure.Extension
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.Configure<BlobOptions>(configuration.GetSection("BlobStorage"));

            BlobOptions? blobOptions = configuration.GetSection("BlobStorage").Get<BlobOptions>();

            if (blobOptions?.ConnectionString == null)
            {
                throw new InvalidOperationException("BlobStorage configuration is missing or invalid.");
            }

            services.AddSingleton(new BlobServiceClient(blobOptions.ConnectionString));
            services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
            services.AddScoped<DatabaseInitializer>();

            return services;
        }
    }
}
