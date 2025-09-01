using Microsoft.EntityFrameworkCore;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Owners.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;

namespace PropertyManager.Infrastructure.Implementations.Persistence.Repositories
{
    public class OwnerRepository(PropertyManagerDbContext context) : GenericRepository<Owner>(context), IOwnerRepository
    {
        public async Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(o => o.UserName == userName, cancellationToken: cancellationToken);
        }

        public  async Task<Owner?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.UserName == userName, cancellationToken: cancellationToken);
        }
    }
}
