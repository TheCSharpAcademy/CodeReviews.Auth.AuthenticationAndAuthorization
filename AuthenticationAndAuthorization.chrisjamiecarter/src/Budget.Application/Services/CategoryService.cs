using System.Linq.Expressions;
using System.Transactions;
using Budget.Application.Repositories;
using Budget.Domain.Entities;
using Budget.Domain.Services;

namespace Budget.Application.Services;

/// <summary>
/// Service class responsible for managing operations related to the Category entity.
/// Provides methods for creating, updating, deleting, and retrieving category data 
/// by interacting with the underlying data repositories through the Unit of Work pattern.
/// </summary>
public class CategoryService : ICategoryService
{
    #region Fields

    private readonly IUnitOfWork _unitOfWork;

    #endregion
    #region Constructors

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion
    #region Methods

    public async Task<bool> CreateAsync(CategoryEntity category)
    {
        await _unitOfWork.Categories.CreateAsync(category);
        var created = await _unitOfWork.SaveAsync();
        return created > 0;
    }

    public async Task<bool> DeleteAsync(Guid userId, CategoryEntity category)
    {
        await _unitOfWork.Categories.DeleteAsync(userId, category);
        var deleted = await _unitOfWork.SaveAsync();
        return deleted > 0;
    }

    public async Task<IReadOnlyList<CategoryEntity>> ReturnAsync(Guid userId)
    {
        var entities = await _unitOfWork.Categories.ReturnAsync(userId);

        return entities.OrderBy(x => x.Name).ToList();
    }

    public async Task<CategoryEntity?> ReturnAsync(Guid userId, Guid id)
    {
        return await _unitOfWork.Categories.ReturnAsync(userId, id);
    }

    public async Task<bool> UpdateAsync(Guid userId, CategoryEntity category)
    {
        await _unitOfWork.Categories.UpdateAsync(userId, category);
        var updated = await _unitOfWork.SaveAsync();
        return updated > 0;
    }

    #endregion
}
