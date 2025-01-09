using Budget.Application.Repositories;
using Budget.Domain.Entities;
using Budget.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Repositories;

/// <summary>
/// Provides repository operations for managing the Infrastructure layer's Category entity.
/// This class implements the <see cref="ICategoryRepository"/> interface, offering 
/// methods to perform CRUD operations against the database using Entity Framework Core.
/// </summary>
internal class CategoryRepository : ICategoryRepository
{
    #region Fields

    private readonly BudgetDbContext _context;

    #endregion
    #region Constructors

    public CategoryRepository(BudgetDbContext context)
    {
        _context = context;
    }

    #endregion
    #region Methods

    public async Task CreateAsync(CategoryEntity category)
    {
        await _context.Category.AddAsync(category);
    }

    public async Task DeleteAsync(Guid userId, CategoryEntity category)
    {
        var entity = await _context.Category.SingleOrDefaultAsync(x => x.UserId == userId && x.Id == category.Id);
        if (entity is not null)
        {
            _context.Category.Remove(entity);
        }
    }

    public async Task<IReadOnlyList<CategoryEntity>> ReturnAsync(Guid userId)
    {
        return await _context.Category.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<CategoryEntity?> ReturnAsync(Guid userId, Guid id)
    {
        return await _context.Category.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);
    }

    public async Task UpdateAsync(Guid userId, CategoryEntity category)
    {
        var entity = await _context.Category.SingleOrDefaultAsync(x => x.UserId == userId && x.Id == category.Id);
        if (entity is not null)
        {
            _context.Category.Entry(entity).CurrentValues.SetValues(category);
        }
    }

    #endregion
}
