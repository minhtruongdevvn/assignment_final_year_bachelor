namespace AtmAPI.Helpers;

public sealed class HttpStatusException : Exception
{
	public HttpStatusException(
		string? message = null,
		HttpStatusCode status = HttpStatusCode.BadRequest,
		bool useCustomResponse = false

	) : base(message)
	{
		Status = status;
		UseCustomResponse = useCustomResponse;
	}

	public HttpStatusException(
		object? customResponse,
		HttpStatusCode status = HttpStatusCode.BadRequest
	) : base(null)
	{
		Status = status;
		CustomResponse = customResponse;
		UseCustomResponse = true;
	}

	public HttpStatusCode Status { get; }
	public bool UseCustomResponse { get; }
	public object? CustomResponse { get; }
}
