using Budget.Application.Repositories;
using Budget.Infrastructure.Contexts;
using Budget.Infrastructure.Entities;
using Budget.Infrastructure.Repositories;
using Budget.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Budget.Infrastructure.Installers;

/// <summary>
/// Registers dependencies and seeds data for the Infrastructure layer.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("Budget") ?? throw new InvalidOperationException("Connection string 'Budget' not found");

        services.AddDbContext<BudgetDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddDefaultIdentity<BudgetUserEntity>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<BudgetDbContext>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISeederService, SeederService>();

        services.AddLogging(config =>
        {
            var sqllogger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.MSSqlServer(
                connectionString: connectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "Log",
                    SchemaName = "audit",
                    AutoCreateSqlDatabase = true,
                    AutoCreateSqlTable = true,
                },
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
                formatProvider: null,
                columnOptions: null,
                logEventFormatter: null)
            .CreateLogger();

            config.AddSerilog(sqllogger);
        });

        return services;
    }

    public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<BudgetDbContext>();
        context.Database.Migrate();

        var seeder = serviceProvider.GetRequiredService<ISeederService>();
        seeder.SeedDatabase();

        return serviceProvider;
    }
}
