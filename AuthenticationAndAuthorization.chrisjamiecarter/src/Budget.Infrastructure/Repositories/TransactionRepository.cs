using Budget.Application.Repositories;
using Budget.Domain.Entities;
using Budget.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Repositories;

/// <summary>
/// Provides repository operations for managing the Infrastructure layer's Transaction entity.
/// This class implements the <see cref="ITransactionRepository"/> interface, offering 
/// methods to perform CRUD operations against the database using Entity Framework Core.
/// </summary>
internal class TransactionRepository : ITransactionRepository
{
    #region Fields

    private readonly BudgetDbContext _context;

    #endregion
    #region Constructors

    public TransactionRepository(BudgetDbContext context)
    {
        _context = context;
    }

    #endregion
    #region Methods

    public async Task CreateAsync(TransactionEntity transaction)
    {
        await _context.Transaction.AddAsync(transaction);
    }

    public async Task DeleteAsync(Guid userId, TransactionEntity transaction)
    {
        var entity = await _context.Transaction.Include(i => i.Category).SingleOrDefaultAsync(x => x.Category!.UserId == userId && x.Id == transaction.Id);
        if (entity is not null)
        {
            _context.Transaction.Remove(entity);
        }
    }

    public async Task<IReadOnlyList<TransactionEntity>> ReturnAsync(Guid userId)
    {
        return await _context.Transaction.Include(i => i.Category).Where(x => x.Category!.UserId == userId).ToListAsync();
    }

    public async Task<TransactionEntity?> ReturnAsync(Guid userId, Guid id)
    {
        return await _context.Transaction.Include(i => i.Category).SingleOrDefaultAsync(x => x.Category!.UserId == userId && x.Id == id);
    }

    public async Task UpdateAsync(Guid userId, TransactionEntity transaction)
    {
        var entity = await _context.Transaction.Include(i => i.Category).SingleOrDefaultAsync(x => x.Category!.UserId == userId && x.Id == transaction.Id);
        if (entity is not null)
        {
            _context.Transaction.Entry(entity).CurrentValues.SetValues(transaction);
        }
    }

    #endregion
}
