using System.Security.Claims;
using Budget.Domain.Entities;
using Budget.Domain.Services;
using Budget.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Web.Controllers;

/// <summary>
/// Manages the Transaction-related actions for the Presentation layer.
/// This controller handles the CRUD operations and also provides filtering and sorting functionalities.
/// </summary>
[Authorize]
public class TransactionsController : Controller
{
    #region Fields

    private readonly ICategoryService _categoryService;
    private readonly ITransactionService _transactionService;
    private readonly ILogger _logger;

    #endregion
    #region Constructors

    public TransactionsController(ICategoryService categoryService, ITransactionService transactionService, ILogger<TransactionsController> logger)
    {
        _categoryService = categoryService;
        _transactionService = transactionService;
        _logger = logger;
    }

    #endregion
    #region Methods

    // GET: Transactions
    public async Task<IActionResult> Index(string searchName, string searchStart, string searchEnd, string filterCategory)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return View(new List<TransactionViewModel>());
        }

        var entities = await _transactionService.ReturnAsync(userId, searchName, searchStart, searchEnd, filterCategory);

        var viewModel = new TransactionsViewModel();

        if (!string.IsNullOrWhiteSpace(searchName))
        {
            viewModel.SearchName = searchName;
        }

        if (!string.IsNullOrWhiteSpace(searchStart))
        {
            viewModel.SearchStart = searchStart;
        }

        if (!string.IsNullOrWhiteSpace(searchEnd))
        {
            viewModel.SearchEnd = searchEnd;
        }

        if (!string.IsNullOrWhiteSpace(filterCategory))
        {
            viewModel.FilterCategory = filterCategory;
        }

        viewModel.SetCategories(await GetCategoriesAsync(userId));
        viewModel.Transactions = entities.Select(x => new TransactionViewModel(x));

        return View(viewModel);
    }

    // GET: Transactions/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return View(new List<CategoryViewModel>());
        }

        var entity = await _transactionService.ReturnAsync(userId, id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var transaction = new TransactionViewModel(entity);
        return Ok(transaction);
    }

    // GET: Transactions/Create
    public async Task<IActionResult> Create()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return NotFound();
        }

        var categories = await GetCategoriesAsync(userId);

        var viewModel = new TransactionViewModel(categories);

        return PartialView("_CreatePartial", viewModel);
    }

    // POST: Transactions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Date,Amount,CategoryId")] TransactionViewModel transaction)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var category = await _categoryService.ReturnAsync(userId, transaction.CategoryId);
            if (category is null)
            {
                return NotFound();
            }

            var newTransaction = new TransactionEntity
            {
                Id = Guid.CreateVersion7(),
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
                Name = transaction.Name,
            };

            await _transactionService.CreateAsync(newTransaction);
            return Json(new { success = true });
        }

        // Reset the SelectList for #Reasons...
        transaction.SetCategories(await GetCategoriesAsync(userId));

        return PartialView("_CreatePartial", transaction);
    }

    // GET: Transactions/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return NotFound();
        }

        var entity = await _transactionService.ReturnAsync(userId, id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var categories = await GetCategoriesAsync(userId);
        var viewModel = new TransactionViewModel(entity, categories);
        return PartialView("_EditPartial", viewModel);
    }

    // POST: Transactions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Date,Amount,CategoryId")] TransactionViewModel transaction)
    {
        if (id != transaction.Id)
        {
            return NotFound();
        }

        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var category = await _categoryService.ReturnAsync(userId, transaction.CategoryId);
            if (category is null)
            {
                return NotFound();
            }
            transaction.Category = new CategoryViewModel(category);
            await _transactionService.UpdateAsync(userId, transaction.MapToDomain(userId));
            return Json(new { success = true });
        }

        // Reset the SelectList for #Reasons...
        transaction.SetCategories(await GetCategoriesAsync(userId));

        return PartialView("_EditPartial", transaction);
    }

    // GET: Transactions/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return View(new List<CategoryViewModel>());
        }

        var entity = await _transactionService.ReturnAsync(userId, id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var transaction = new TransactionViewModel(entity);
        return PartialView("_DeletePartial", transaction);
    }

    // POST: Transactions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return NotFound();
        }

        var entity = await _transactionService.ReturnAsync(userId, id);
        if (entity is null)
        {
            return NotFound();
        }

        await _transactionService.DeleteAsync(userId, entity);
        return Json(new { success = true });
    }

    private async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync(Guid userId)
    {
        var entities = await _categoryService.ReturnAsync(userId);
        return entities.Select(x => new CategoryViewModel(x));
    }

    #endregion
}
