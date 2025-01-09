namespace Budget.Domain.Entities;

/// <summary>
/// Represents a Transaction entity within the Domain layer.
/// </summary>
public class TransactionEntity : EntityBase
{
    #region Properties

    public required string Name { get; set; }

    public required DateTime Date { get; set; }

    public required decimal Amount { get; set; }

    public required Guid CategoryId { get; set; }

    public CategoryEntity? Category { get; set; }

    #endregion
}
