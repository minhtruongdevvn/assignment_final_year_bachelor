namespace AtmAPI.Extensions;

/// <summary>
///     High performance wrapper of logger factory extensions.
/// </summary>
public static partial class LoggerMessageDefinition
{
	private const string BaseTemplate = "{Message}\n";
	private const string TabStr = "      ";

	[LoggerMessage(1, LogLevel.Information, BaseTemplate)]
	public static partial void Info(this ILogger logger, string message);

	[LoggerMessage(2, LogLevel.Debug, BaseTemplate)]
	public static partial void Debug(this ILogger logger, string message);

	[LoggerMessage(3, LogLevel.Warning, BaseTemplate)]
	public static partial void Warn(this ILogger logger, string message);

	[LoggerMessage(4, LogLevel.Error, BaseTemplate)]
	public static partial void Error(this ILogger logger, string message);

	[LoggerMessage(5, LogLevel.Critical, BaseTemplate)]
	public static partial void Crit(this ILogger logger, string message);

	[LoggerMessage(666, LogLevel.Error, $"[{{Type}}] {{Message}}\n\n{TabStr}{{Trace}}")]
	public static partial void LogInternalException(
		this ILogger logger,
		string type,
		string message,
		string? trace
	);

	[LoggerMessage(
		EventId = 777,
		Level = LogLevel.Error,
		SkipEnabledCheck = true,
		Message = "[{StatusCode}] {Message}" + $"\n{TabStr}at {{fileName}}:line {{lineNumber}}"
	)]
	public static partial void LogHttpException(
		this ILogger logger,
		HttpStatusCode statusCode,
		string message,
		string? fileName,
		int? lineNumber
	);
}
