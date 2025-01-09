using System.Security.Claims;
using Budget.Domain.Entities;
using Budget.Domain.Services;
using Budget.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget.Web.Controllers;

/// <summary>
/// Manages the Category-related actions for the Presentation layer.
/// This controller handles the CRUD operations.
/// </summary>
[Authorize]
public class CategoriesController : Controller
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

    private readonly ICategoryService _categoryService;
    private readonly ILogger _logger;

    #endregion
    #region Constructors

    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    #endregion
    #region Methods

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return View(new List<CategoryViewModel>());
        }

        var entities = await _categoryService.ReturnAsync(userId);
        var categories = entities.Select(x => new CategoryViewModel(x));

        return View(categories);
    }

    // GET: Categories/Details/5
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

        var entity = await _categoryService.ReturnAsync(userId, id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var category = new CategoryViewModel(entity);
        return Ok(category);
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        var viewModel = new CategoryViewModel();

        return PartialView("_CreatePartial", viewModel);
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] CategoryViewModel category)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return NotFound();
        }

        if (ModelState.IsValid && await IsDuplicateCategoryName(userId, category.Id, category.Name))
        {
            ModelState.AddModelError("Name", "A Categeory with that Name already exists.");
        }

        if (ModelState.IsValid)
        {
            category.Id = Guid.CreateVersion7();
            await _categoryService.CreateAsync(category.MapToDomain(userId));
            return Json(new { success = true });
        }

        return PartialView("_CreatePartial", category);
    }

    public async Task<IActionResult> CreateDefault()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return RedirectToAction("Index", "Categories");
        }

        var existingCategories = await _categoryService.ReturnAsync(userId);

        foreach (var category in _categories)
        {
            if (!existingCategories.Select(x => x.Name).Contains(category))
            {
                await _categoryService.CreateAsync(new CategoryEntity { Id = Guid.CreateVersion7(), Name = category, UserId = userId });
            }
        }

        return RedirectToAction("Index", "Categories");
    }

    // GET: Categories/Edit/5
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

        var entity = await _categoryService.ReturnAsync(userId, id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var viewModel = new CategoryViewModel(entity);
        return PartialView("_EditPartial", viewModel);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] CategoryViewModel category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return NotFound();
        }

        if (ModelState.IsValid && await IsDuplicateCategoryName(userId, category.Id, category.Name))
        {
            ModelState.AddModelError("Name", "A Categeory with that Name already exists.");
        }

        if (ModelState.IsValid)
        {
            await _categoryService.UpdateAsync(userId, category.MapToDomain(userId));
            return Json(new { success = true });
        }

        return PartialView("_EditPartial", category);
    }

    // GET: Categories/Delete/5
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

        var entity = await _categoryService.ReturnAsync(userId, id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var category = new CategoryViewModel(entity);
        return PartialView("_DeletePartial", category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("Unable to get logged in user id");
            return NotFound();
        }

        var entity = await _categoryService.ReturnAsync(userId, id);
        if (entity is null)
        {
            return NotFound();
        }

        await _categoryService.DeleteAsync(userId, entity);
        return Json(new { success = true });
    }

    private async Task<bool> IsDuplicateCategoryName(Guid userId, Guid id, string name)
    {
        var categories = await _categoryService.ReturnAsync(userId);

        var match = categories.FirstOrDefault(c => c.Name!.Equals(name, StringComparison.CurrentCultureIgnoreCase));

        return match is not null && match.Id != id;
    }

    #endregion
}
