using Budget.Domain.Entities;

namespace Budget.Web.Models;

/// <summary>
/// Represents the view model used to display a Category object in the Presentation layer.
/// </summary>
public class CategoryViewModel
{
    #region Constructors

    public CategoryViewModel()
    {

    }

    public CategoryViewModel(CategoryEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name ?? "";
        Transactions = [];
    }

    #endregion
    #region Properties

    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public List<TransactionViewModel>? Transactions { get; set; }

    #endregion
    #region Methods

    public CategoryEntity MapToDomain(Guid userId)
    {
        return new CategoryEntity
        {
            Id = this.Id,
            Name = this.Name,
            UserId = userId,
        };
    }

    #endregion
}
