using Budget.Domain.Entities;

namespace Budget.Domain.Services;

/// <summary>
/// Defines the contract for a service that manages Category entities.
/// </summary>
public interface ICategoryService
{
    Task<bool> CreateAsync(CategoryEntity category);
    Task<bool> DeleteAsync(Guid userId, CategoryEntity category);
    Task<IReadOnlyList<CategoryEntity>> ReturnAsync(Guid userId);
    Task<CategoryEntity?> ReturnAsync(Guid userId, Guid id);
    Task<bool> UpdateAsync(Guid userId, CategoryEntity category);
}