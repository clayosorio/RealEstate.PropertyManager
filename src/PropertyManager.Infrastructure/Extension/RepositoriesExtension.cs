using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication;
using PropertyManager.Domain.Abstractions.Repositories;
using PropertyManager.Domain.Common.Sotrage.Repositories;
using PropertyManager.Domain.Owners.Repositories;
using PropertyManager.Domain.Properties.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;
using PropertyManager.Infrastructure.Implementations.Persistence.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.Sotrage;
using PropertyManager.Infrastructure.Implementations.Security.Authentication;

namespace PropertyManager.Infrastructure.Extension
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPasswordService, PasswordService>();

            return services;
        }
    }
}
