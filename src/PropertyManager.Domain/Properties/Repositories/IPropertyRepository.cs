using PropertyManager.Domain.Abstractions.Repositories;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Queries.Output;

namespace PropertyManager.Domain.Properties.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {

        Task<List<PropertyOutputDto>> GetPropertiesAsync(
        string? name,
        decimal? minPrice,
        decimal? maxPrice,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
        Task<int> GetPropertiesCountAsync(string? name, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken);
    }
}
