using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AtmAPI.Extensions;

public static class ThrowExtensions
{
	private const HttpStatusCode _defaultStatusCode = HttpStatusCode.BadRequest;

	public static Validatable<TValue> ThrowIfNullHttpStatus<TValue>(
		[NotNull] this TValue? value,
		string? message = null,
		HttpStatusCode status = _defaultStatusCode,
		Action<TValue?>? beforeThrow = null,
		[CallerArgumentExpression("value")] string? paramName = null
	) where TValue : notnull =>
		value.ProcessThrowIfNullHttpStatus(
			beforeThrowCallback: beforeThrow,
			paramName: paramName,
			message: message,
			status: status
		);

	public static Validatable<TValue> ThrowHttpStatus<TValue>(
		[DisallowNull, NotNull] this TValue value,
		string? message = null,
		HttpStatusCode status = _defaultStatusCode,
		Action<TValue?>? beforeThrow = null,
		[CallerArgumentExpression("value")] string? paramName = null
	) where TValue : notnull =>
		value.ProcessThrowHttpStatus(
			beforeThrowCallback: beforeThrow,
			paramName: paramName,
			message: message,
			status: status
		);

	public static Validatable<TValue> ThrowIfNullHttpStatus<TValue>(
		[NotNull] this TValue? value,
		object? customResponse,
		HttpStatusCode status = _defaultStatusCode,
		Action<TValue?>? beforeThrow = null,
		[CallerArgumentExpression("value")] string? paramName = null
	) where TValue : notnull =>
		value.ProcessThrowIfNullHttpStatus(
			customResponse: customResponse,
			beforeThrowCallback: beforeThrow,
			paramName: paramName,
			status: status
		);

	public static Validatable<TValue> ThrowHttpStatus<TValue>(
		[DisallowNull, NotNull] this TValue value,
		object? customResponse,
		HttpStatusCode status = _defaultStatusCode,
		Action<TValue?>? beforeThrow = null,
		[CallerArgumentExpression("value")] string? paramName = null
	) where TValue : notnull =>
		value.ProcessThrowHttpStatus(
			customResponse: customResponse,
			beforeThrowCallback: beforeThrow,
			paramName: paramName,
			status: status
		);

	private static Validatable<TValue> ProcessThrowIfNullHttpStatus<TValue>(
		[NotNull] this TValue? value,
		string? message = null,
		object? customResponse = null,
		HttpStatusCode status = _defaultStatusCode,
		Action<TValue?>? beforeThrowCallback = null,
		[CallerArgumentExpression("value")] string? paramName = null
	) where TValue : notnull
	{
		var beforeThrow = () => beforeThrowCallback?.Invoke(value);
		var exceptionThrower = ExceptionThrower(message, customResponse, status, beforeThrow);

		return value == null
			? throw exceptionThrower()
			: new(value, paramName ?? $"param (type: {typeof(TValue)})", exceptionThrower);
	}

	private static Validatable<TValue> ProcessThrowHttpStatus<TValue>(
		[DisallowNull, NotNull] this TValue value,
		string? message = null,
		object? customResponse = null,
		HttpStatusCode status = _defaultStatusCode,
		Action<TValue?>? beforeThrowCallback = null,
		[CallerArgumentExpression("value")] string? paramName = null
	) where TValue : notnull
	{
		var beforeThrow = () => beforeThrowCallback?.Invoke(value);

		return new(
			value,
			paramName ?? $"param (type: {typeof(TValue)})",
			ExceptionThrower(message, customResponse, status, beforeThrow)
		);
	}

	private static Func<HttpStatusException> ExceptionThrower(
		string? message = null,
		object? customResponse = null,
		HttpStatusCode status = _defaultStatusCode,
		Action? beforeThrowAction = null
	)
	{
		Func<HttpStatusException> exceptionThrower = () =>
		{
			beforeThrowAction?.Invoke();
			return new(null, status);
		};

		if (message != null)
			exceptionThrower = () => new(message, status, true);
		else if (customResponse != null)
			exceptionThrower = () => new(customResponse, status);

		return exceptionThrower;
	}
}
