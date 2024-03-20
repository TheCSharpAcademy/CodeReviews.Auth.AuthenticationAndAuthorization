namespace Movies.StevieTV.Logging;

public static class DbLoggerExtensions
{
    public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerOptions> configure)  
    {            
        builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();  
        builder.Services.Configure(configure);  
        return builder;  
    }  
}