using Budget.Domain.Entities;

namespace Budget.Domain.Services;

/// <summary>
/// Defines the contract for a service that manages Transaction entities.
/// </summary>
public interface ITransactionService
{
    Task<bool> CreateAsync(TransactionEntity transaction);
    Task<bool> DeleteAsync(Guid userId, TransactionEntity transaction);
    Task<IReadOnlyList<TransactionEntity>> ReturnAsync(Guid userId, string searchName, string searchStart, string searchEnd, string filterCategory);
    Task<TransactionEntity?> ReturnAsync(Guid userId, Guid id);
    Task<bool> UpdateAsync(Guid userId, TransactionEntity transaction);
}