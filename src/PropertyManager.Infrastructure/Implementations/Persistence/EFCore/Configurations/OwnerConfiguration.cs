using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.Owners.Entities;

namespace PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owner");

            builder.HasKey(o => o.IdOwner);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.Address)
                .HasMaxLength(300);

            builder.Property(o => o.Photo)
                .HasMaxLength(500);

            builder.Property(o => o.Birthday)
                .HasColumnType("date");

            builder.Property(o => o.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(o => o.UserName).IsUnique();

            builder.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(o => o.Email).IsUnique();

            builder.Property(o => o.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(o => o.PasswordSalt)
                .HasMaxLength(500);

            builder.HasMany(o => o.Properties)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.IdOwner)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
