using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;

namespace PropertyManager.Infrastructure.Implementations.Persistence.Repositories
{
    public class PropertyRepository(PropertyManagerDbContext context) 
        : GenericRepository<Property>(context), IPropertyRepository
    {
    }
}
