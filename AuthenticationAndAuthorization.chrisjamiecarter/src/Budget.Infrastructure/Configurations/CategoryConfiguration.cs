using Budget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budget.Infrastructure.Configurations;

/// <summary>
/// Custom configuration for the <see cref="CategoryEntity"/> type.
/// </summary>
internal class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
    }
}
