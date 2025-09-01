using PropertyManager.Domain.PropertyImages.Entities;
using PropertyManager.Domain.PropertyImages.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;

namespace PropertyManager.Infrastructure.Implementations.Persistence.Repositories
{
    public class PropertyImageRepository(PropertyManagerDbContext context) 
        : GenericRepository<PropertyImage>(context), IPropertyImageRepository
    {
    }
}
