namespace Budget.Domain.Entities;

/// <summary>
/// Represents a base entity within the Domain layer.
/// </summary>
public abstract class EntityBase
{
    #region Properties

    public required Guid Id { get; set; }

    #endregion
}
