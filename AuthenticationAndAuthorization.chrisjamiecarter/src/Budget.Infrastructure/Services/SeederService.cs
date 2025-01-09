using Bogus;
using Budget.Domain.Entities;
using Budget.Infrastructure.Contexts;
using Budget.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Services;

/// <summary>
/// Provides methods to seed the database with initial data.
/// This service adds a defined set of default Category records
/// and then uses Bogus to add fake Transaction records.
/// </summary>
internal class SeederService : ISeederService
{
    #region Fields

    private readonly string[] _categories =
    [
        "Bills",
        "Charity",
        "Eating Out",
        "Entertainment",
        "Expenses",
        "Family",
        "Finances",
        "General",
        "Gifts",
        "Groceries",
        "Holidays",
        "Personal Care",
        "Savings",
        "Shopping",
        "Transfers",
        "Transport"
    ];

    private readonly BudgetDbContext _context;
    private readonly UserManager<BudgetUserEntity> _userManager;

    #endregion
    #region Constructors

    public SeederService(BudgetDbContext context, UserManager<BudgetUserEntity> userManager)
    {
        _context = context;
        _userManager = userManager;
        Randomizer.Seed = new Random(19890309);
    }

    #endregion
    #region Methods

    public void SeedDatabase()
     {
        var users = SeedUsers().Result;

        foreach (var user in users)
        {
            Guid userId = Guid.Parse(user.Id);

            SeedCategories(userId);

            SeedTransactions(userId);
        }
    }

    private void SeedCategories(Guid userId)
    {
        foreach (var category in _categories)
        {
            _context.Category.Add(new CategoryEntity { Id = Guid.CreateVersion7(), Name = category, UserId = userId });
        }
        _context.SaveChanges();
    }

    private void SeedTransactions(Guid userId)
    {
        var categories = _context.Category.AsNoTracking().Where(x => x.UserId == userId).ToList();

        var fakeTransactions = new Faker<TransactionEntity>()
            .RuleFor(t => t.Id, f => Guid.CreateVersion7())
            .RuleFor(t => t.CategoryId, f => f.PickRandom(categories).Id)
            .RuleFor(t => t.Date, f => f.Date.Past(1))
            .RuleFor(t => t.Name, (f, t) => f.Commerce.Product())
            .RuleFor(t => t.Amount, f => f.Finance.Amount(0.01M));

        foreach (var transaction in fakeTransactions.Generate(100))
        {
            _context.Transaction.Add(transaction);
        }

        _context.SaveChanges();
    }

    private async Task<IReadOnlyList<BudgetUserEntity>> SeedUsers()
    {
        if (_userManager.Users.Any())
        {
            return [];
        }

        await _userManager.CreateAsync(new BudgetUserEntity
        {
            UserName = "admin@email.com"
        }, "adminADMIN123;'#");

        await _userManager.CreateAsync(new BudgetUserEntity
        {
            UserName = "user@email.com"
        }, "userUSER123;'#");

        return await _userManager.Users.ToListAsync();
    }

    #endregion
}
