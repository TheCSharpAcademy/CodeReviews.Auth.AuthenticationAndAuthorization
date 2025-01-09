namespace Budget.Domain.Entities;

/// <summary>
/// Represents a Category entity within the Domain layer.
/// </summary>
public class CategoryEntity : EntityBase
{
    #region Properties

    public required string Name { get; set; }

    public required Guid UserId { get; set; }

    public ICollection<TransactionEntity> Transactions { get; set; } = [];

    #endregion
}
