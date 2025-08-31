using Microsoft.EntityFrameworkCore.Storage;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Domain.Abstractions.Repositories;
using PropertyManager.Infrastructure.Implementations.Persistence.Repositories;

namespace PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context
{
    public class UnitOfWork(PropertyManagerDbContext context) : IUnitOfWork
    {
        private readonly PropertyManagerDbContext _context = context;
        private IDbContextTransaction? _transaction;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories.TryGetValue(typeof(TEntity), out var repo))
                return (IGenericRepository<TEntity>)repo;

            var repository = new GenericRepository<TEntity>(_context);
            _repositories[typeof(TEntity)] = repository;
            return repository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }
    }
}
