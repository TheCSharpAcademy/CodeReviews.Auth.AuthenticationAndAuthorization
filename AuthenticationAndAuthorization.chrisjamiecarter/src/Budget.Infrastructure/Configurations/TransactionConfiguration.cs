using Budget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budget.Infrastructure.Configurations;

/// <summary>
/// Custom configuration for the <see cref="TransactionEntity"/> type.
/// </summary>
internal class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();

        builder.HasOne(t => t.Category)
               .WithMany(c => c.Transactions)
               .HasForeignKey(fk => fk.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
