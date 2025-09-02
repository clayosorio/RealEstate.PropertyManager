using Microsoft.EntityFrameworkCore;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Queries.Output;
using PropertyManager.Domain.Properties.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;

namespace PropertyManager.Infrastructure.Implementations.Persistence.Repositories
{
    public class PropertyRepository(PropertyManagerDbContext context)
        : GenericRepository<Property>(context), IPropertyRepository
    {
        public async Task<List<PropertyOutputDto>> GetPropertiesAsync(
        string? name,
        decimal? minPrice,
        decimal? maxPrice,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        {
            var query = _context.Properties.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            var properties = await query
                .OrderBy(p => p.IdProperty)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PropertyOutputDto
                {
                    IdProperty = p.IdProperty,
                    Name = p.Name,
                    Address = p.Address,
                    Price = p.Price,
                    IdOwner = p.IdOwner,
                    OwnerName = p.Owner.Name,
                    Year = p.Year,
                    CodeInternal = p.CodeInternal,
                    CreatedAt = p.CreatedAt,
                })
                .ToListAsync(cancellationToken);

            return properties;
        }

        public async Task<int> GetPropertiesCountAsync(string? name, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken)
        {
            var query = _context.Properties.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            return await query.CountAsync(cancellationToken);
        }
    }
}
