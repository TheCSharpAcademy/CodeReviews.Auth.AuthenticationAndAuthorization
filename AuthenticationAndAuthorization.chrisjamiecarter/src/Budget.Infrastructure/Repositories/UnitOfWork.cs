using Budget.Application.Repositories;
using Budget.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Budget.Infrastructure.Repositories;

/// <summary>
/// The UnitOfWork class provides a central point for managing database transactions and
/// saving changes across multiple repositories. It coordinates changes in 
/// <see cref="ICategoryRepository"/> and <see cref="ITransactionRepository"/>.
/// </summary>
/// <remarks>
/// This class follows the Unit of Work design pattern, ensuring that all repository operations 
/// are treated as a single transaction, maintaining data consistency.
/// </remarks>
internal class UnitOfWork : IUnitOfWork
{
    #region Fields

    private readonly BudgetDbContext _context;
    private readonly ILogger _logger;

    #endregion
    #region Constructors

    public UnitOfWork(BudgetDbContext context, ICategoryRepository categoryRepository, ITransactionRepository transactionRepository, ILogger<UnitOfWork> logger)
    {
        _context = context;
        Categories = categoryRepository;
        Transactions = transactionRepository;
        _logger = logger;
    }

    #endregion
    #region Properties

    public ICategoryRepository Categories { get; set; }

    public ITransactionRepository Transactions { get; set; }

    #endregion
    #region Methods

    public async Task<int> SaveAsync()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception.Message);
            return -1;
        }
    }

    #endregion
}
