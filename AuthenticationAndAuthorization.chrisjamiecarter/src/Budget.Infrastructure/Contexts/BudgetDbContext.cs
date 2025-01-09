using Budget.Domain.Entities;
using Budget.Infrastructure.Configurations;
using Budget.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Contexts;

/// <summary>
/// Represents the Entity Framework Core database context for the Budget data store.
/// </summary>
internal class BudgetDbContext : IdentityDbContext<BudgetUserEntity>
{
    #region Constructors

    public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options) { }

    #endregion
    #region Properties

    public DbSet<CategoryEntity> Category { get; set; } = default!;

    public DbSet<TransactionEntity> Transaction { get; set; } = default!;

    #endregion
    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
    }

    #endregion
}
