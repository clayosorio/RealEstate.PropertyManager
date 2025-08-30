using Microsoft.EntityFrameworkCore;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.PropertyImages.Entities;
using PropertyManager.Domain.PropertyTraces.Entities;
using PropertyManager.Domain.Users.Entities;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Configurations;

namespace PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context
{
    public class PropertyManagerDbContext(DbContextOptions<PropertyManagerDbContext> options) : DbContext(options)
    {
        public readonly string _connectionString;

        public PropertyManagerDbContext(string connectionString)
            : this(new DbContextOptions<PropertyManagerDbContext>())
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region [Configuration]
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyImageConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTraceConfiguration());
            #endregion
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }
    }
}
