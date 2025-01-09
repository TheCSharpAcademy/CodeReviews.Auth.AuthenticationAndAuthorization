namespace Budget.Infrastructure.Services;

/// <summary>
/// Defines the contract for a service that manages Transaction entities.
/// </summary>
internal interface ISeederService
{
    void SeedDatabase();
}