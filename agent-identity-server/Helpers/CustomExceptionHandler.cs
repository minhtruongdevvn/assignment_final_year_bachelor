using System.Dynamic;
using Microsoft.AspNetCore.Diagnostics;

namespace AgentIdentityServer.Helpers;

public static class CustomExceptionHandler
{
	private static readonly dynamic _response = new ExpandoObject();

	public static Action<IApplicationBuilder> ActionBuilder =>
		handler =>
			handler.Run(async ctx =>
			{
				var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
				if (ex == null) return;

				_response.messages = "Something went wrong. Please try again later.";
				ctx.Response.ContentType = MediaTypeNames.Application.Json;
				ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;

				// internal system error logging only:
				LoggerHelpers.Logger.Error(ex.ToString());

				await ctx.Response.WriteAsync(
					(string)NtsJson.JsonConvert.SerializeObject(_response)
				);
			});
}
