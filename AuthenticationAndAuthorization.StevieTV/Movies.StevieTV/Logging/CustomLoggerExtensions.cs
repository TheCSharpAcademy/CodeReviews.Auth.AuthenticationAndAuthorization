namespace Movies.StevieTV.Logging;

public static class CustomLoggerExtensions
{
    public static IServiceCollection AddCustomLogger(this IServiceCollection services)
    {
        services.AddScoped<ILogger, CustomLogger>();
        return services;
    }
}