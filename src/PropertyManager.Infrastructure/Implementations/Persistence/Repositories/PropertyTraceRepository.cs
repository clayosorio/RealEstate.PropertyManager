using PropertyManager.Domain.PropertyTraces.Entities;
using PropertyManager.Domain.PropertyTraces.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;

namespace PropertyManager.Infrastructure.Implementations.Persistence.Repositories
{
    public class PropertyTraceRepository(PropertyManagerDbContext context) 
        : GenericRepository<PropertyTrace>(context), IPropertyTraceRepository
    {
    }
}
