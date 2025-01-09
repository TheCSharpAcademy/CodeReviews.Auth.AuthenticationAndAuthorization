using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.Web.Models;

/// <summary>
/// Represents the view model used to display a list of Transaction view models in the Presentation layer.
/// </summary>
public class TransactionsViewModel
{
    public IEnumerable<TransactionViewModel> Transactions { get; set; } = [];

    public IEnumerable<SelectListItem> Categories { get; set; } = [];

    [Display(Name = "Filter")]
    public string? SearchName { get; set; }

    [DataType(DataType.Date), Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public string? SearchStart { get; set; }

    [DataType(DataType.Date), Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public string? SearchEnd { get; set; }

    [Display(Name = "Category")]
    public string? FilterCategory { get; set; }

    public void SetCategories(IEnumerable<CategoryViewModel> categories)
    {
        Categories = categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
    }
}
