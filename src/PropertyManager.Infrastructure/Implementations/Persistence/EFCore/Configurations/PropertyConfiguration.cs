using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.Properties.Entities;

namespace PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Property");

            builder.HasKey(p => p.IdProperty);

            builder.Property(p => p.IdProperty)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Address)
                .HasMaxLength(300);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CodeInternal)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.CodeInternal).IsUnique();

            builder.Property(p => p.CreatedAt)
                .HasColumnType("datetime");

            builder.Property(p => p.UpdatedAt)
                .HasColumnType("datetime");

            builder.Property(p => p.Year)
                .IsRequired();

            builder.HasOne(p => p.Owner)
                .WithMany(o => o.Properties)
                .HasForeignKey(p => p.IdOwner);

            builder.HasMany(p => p.PropertyImages)
                .WithOne(pi => pi.Property)
                .HasForeignKey(pi => pi.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PropertyTraces)
                .WithOne(pt => pt.Property)
                .HasForeignKey(pt => pt.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
