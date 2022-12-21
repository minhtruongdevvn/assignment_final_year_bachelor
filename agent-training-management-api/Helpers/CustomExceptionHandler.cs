using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Sieve.Exceptions;

namespace AtmAPI.Helpers;

public static class CustomExceptionHandler
{
	private static int Code = StatusCodes.Status500InternalServerError;
	private static object? Response;

	public static Action<IApplicationBuilder> ActionBuilder =>
		handler =>
			handler.Run(async ctx =>
			{
				var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
				if (ex == null) return;
				Response = new { messages = "Something went wrong. Please try again later." };

				if (ex is HttpStatusException httpEx)
					HandleHttpStatusException(httpEx);
				else if (ex is DbUpdateException dbEx)
					HandleDbUpdateException(dbEx);
				else if (ex is SieveMethodNotFoundException sieveNotFoundEx)
				{
					Code = StatusCodes.Status400BadRequest;
					Response = new { messages = sieveNotFoundEx.Message };
				}
				else {
					LoggerHelpers.Logger.Error(ex.ToString());
				}

				ctx.Response.StatusCode = Code;
				ctx.Response.ContentType = MediaTypeNames.Application.Json;

				if (Response != null)
					await ctx.Response.WriteAsync(JsonConvert.SerializeObject(Response));
			});

	public static void HandleDbUpdateException(DbUpdateException dbEx)
	{
		// 2601: Duplicity errors in entities
		// 2627: Duplicity errors in entities's sub-entities
		var errorNumbers = new[] { 2601, 2627 };
		Code = StatusCodes.Status400BadRequest;
		if (dbEx.InnerException is SqlException sqlEx && errorNumbers.Contains(sqlEx.Number))
			Response = new { messages = sqlEx.Errors.OfType<SqlError>().Select(e => e.Message) };
	}

	public static void HandleHttpStatusException(HttpStatusException httpEx)
	{
		Code = (int)httpEx.Status;

		if (Code < 300)
		{
			Response = null;
			return;
		}

		var defaultResponse = new { messages = httpEx.Message };
		var customResponse = httpEx.CustomResponse ?? defaultResponse;
		Response = httpEx.UseCustomResponse ? customResponse : defaultResponse;
	}
}
