namespace AgentIdentityServer.Extensions;

public static partial class LoggerMessageDefinition
{
	private const string Suffix = "\n";
	private const string BaseTmpl = "{Message}" + Suffix;
	private const string TabStr = "      ";

	[LoggerMessage(1, LogLevel.Information, BaseTmpl)]
	public static partial void Info(this ILogger logger, string message);

	[LoggerMessage(2, LogLevel.Debug, BaseTmpl)]
	public static partial void Debug(this ILogger logger, string message);

	[LoggerMessage(3, LogLevel.Warning, BaseTmpl)]
	public static partial void Warn(this ILogger logger, string message);

	[LoggerMessage(4, LogLevel.Error, BaseTmpl)]
	public static partial void Error(this ILogger logger, string message);

	[LoggerMessage(5, LogLevel.Critical, BaseTmpl)]
	public static partial void Crit(this ILogger logger, string message);

	[LoggerMessage(666, LogLevel.Error, $"[{{Type}}] {{Message}}\n{TabStr}{{Trace}}{Suffix}")]
	public static partial void LogInternalException(
		this ILogger logger,
		string type,
		string message,
		string? trace
	);
}
