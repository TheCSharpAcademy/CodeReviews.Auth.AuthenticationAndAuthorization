using Budget.Application.Repositories;
using Budget.Domain.Entities;
using Budget.Domain.Services;

namespace Budget.Application.Services;

/// <summary>
/// Service class responsible for managing operations related to the Transaction entity.
/// Provides methods for creating, updating, deleting, and retrieving category data 
/// by interacting with the underlying data repositories through the Unit of Work pattern.
/// </summary>
public class TransactionService : ITransactionService
{
    #region Fields

    private readonly IUnitOfWork _unitOfWork;

    #endregion
    #region Constructors

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion
    #region Methods

    public async Task<bool> CreateAsync(TransactionEntity transaction)
    {
        await _unitOfWork.Transactions.CreateAsync(transaction);
        var created = await _unitOfWork.SaveAsync();
        return created > 0;
    }

    public async Task<bool> DeleteAsync(Guid userId, TransactionEntity transaction)
    {
        await _unitOfWork.Transactions.DeleteAsync(userId, transaction);
        var deleted = await _unitOfWork.SaveAsync();
        return deleted > 0;
    }

    public async Task<IReadOnlyList<TransactionEntity>> ReturnAsync(Guid userId, string searchName, string searchStart, string searchEnd, string filterCategory)
    {
        var entities = await _unitOfWork.Transactions.ReturnAsync(userId);

        var query = entities.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchName))
        {
            query = query.Where(e => e.Name!.Contains(searchName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(searchStart))
        {
            var startDate = DateTime.Parse(searchStart);
            query = query.Where(e => e.Date >= startDate);
        }

        if (!string.IsNullOrWhiteSpace(searchEnd))
        {
            var endDate = DateTime.Parse(searchEnd);
            query = query.Where(e => e.Date <= endDate);
        }

        if (!string.IsNullOrWhiteSpace(filterCategory))
        {
            query = query.Where(e => e.Category!.Id == Guid.Parse(filterCategory));
        }

        query = query.OrderBy(e => e.Date);

        return query.ToList();
    }

    public async Task<TransactionEntity?> ReturnAsync(Guid userId, Guid id)
    {
        return await _unitOfWork.Transactions.ReturnAsync(userId, id);
    }

    public async Task<bool> UpdateAsync(Guid userId, TransactionEntity transaction)
    {
        await _unitOfWork.Transactions.UpdateAsync(userId, transaction);
        var updated = await _unitOfWork.SaveAsync();
        return updated > 0;
    }

    #endregion
}
