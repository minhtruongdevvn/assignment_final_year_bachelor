namespace AgentIdentityServer.Helpers;

public static class LoggerHelpers
{
	public static IServiceProvider ServiceProvider { get; set; } = default!;
	public static ILogger<LogErrorHolder> Logger
	{
		get => ServiceProvider.CreateScope()
			.ServiceProvider.GetRequiredService<ILogger<LogErrorHolder>>();
	}
}

public class LogErrorHolder { }
