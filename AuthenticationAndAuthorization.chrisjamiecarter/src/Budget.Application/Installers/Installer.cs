using Budget.Application.Services;
using Budget.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Budget.Application.Installers;

/// <summary>
/// Registers dependencies for the Application layer.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
