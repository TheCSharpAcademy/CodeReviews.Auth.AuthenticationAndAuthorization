using System.Linq.Expressions;
using Budget.Domain.Entities;

namespace Budget.Application.Repositories;

/// <summary>
/// Defines the contract for accessing and managing Transaction entities in the data store for the Application.
/// </summary>
public interface ITransactionRepository
{
    Task CreateAsync(TransactionEntity transaction);
    Task DeleteAsync(Guid userId, TransactionEntity transaction);
    Task<IReadOnlyList<TransactionEntity>> ReturnAsync(Guid userId);
    Task<TransactionEntity?> ReturnAsync(Guid userId, Guid id);
    Task UpdateAsync(Guid userId, TransactionEntity transaction);
}
