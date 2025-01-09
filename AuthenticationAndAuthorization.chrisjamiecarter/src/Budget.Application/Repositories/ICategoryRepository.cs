using System.Linq.Expressions;
using Budget.Domain.Entities;

namespace Budget.Application.Repositories;

/// <summary>
/// Defines the contract for accessing and managing Category entities in the data store for the Application.
/// </summary>
public interface ICategoryRepository
{
    Task CreateAsync(CategoryEntity category);
    Task DeleteAsync(Guid userId, CategoryEntity category);
    Task<IReadOnlyList<CategoryEntity>> ReturnAsync(Guid userId);
    Task<CategoryEntity?> ReturnAsync(Guid userId, Guid id);
    Task UpdateAsync(Guid userId, CategoryEntity category);
}
