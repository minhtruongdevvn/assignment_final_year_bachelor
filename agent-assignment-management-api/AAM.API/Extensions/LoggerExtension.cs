namespace AAM.API;

public static class LoggerExtension
{
    public static IServiceProvider ServiceProvider { get; set; } = default!;
    public static ILogger<LogErrorHolder> Logger
    {
        get => ServiceProvider.CreateScope()
            .ServiceProvider.GetRequiredService<ILogger<LogErrorHolder>>();
    }
}

public class LogErrorHolder { }