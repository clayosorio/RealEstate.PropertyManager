using PropertyManager.Domain.Abstractions.Repositories;
using PropertyManager.Domain.Owners.Entities;

namespace PropertyManager.Domain.Owners.Repositories
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken);
        Task<Owner?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    }
}
