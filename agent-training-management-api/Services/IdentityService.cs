using AtmAPI.Extensions;
using Flurl;
using Flurl.Http;
using System.Text;
using static Sieve.Extensions.MethodInfoExtended;

namespace AtmAPI.Services;

public class IdentityService
{
	private readonly AccessTokenProvider _accessTokenProvider;

	public IdentityService(AccessTokenProvider accessTokenProvider)
	{
		_accessTokenProvider = accessTokenProvider;
	}

	public async Task<IdentityClientResponse> CreateUserAsync(
		UserUpsertRequest request,
		string role
	)
	{
		return await HandleRequestAsync(
			"create",
			@return: true,
			request: (token) =>
				Flurl($"api/users/{role}")
					.WithHeader("Content-Type", "application/json")
					.WithOAuthBearerToken(token)
					.PostJsonAsync(request.ToSnakeCaseProps())
					.ReceiveJson()
		);
	}

	public async Task<IdentityClientResponse> UpdateUserAsync(int userId, UserUpsertRequest request)
	{
		return await HandleRequestAsync(
			"update",
			(token) =>
				Flurl($"api/users/{userId}")
					.WithHeader("Content-Type", "application/json")
					.WithOAuthBearerToken(token)
					.PutJsonAsync(request.ToSnakeCaseProps())
					.ReceiveJson()
		);
	}

	public async Task<IdentityClientResponse> DeleteUserAsync(int userId)
	{
		return await HandleRequestAsync(
			"delete",
			(token) =>
				Flurl($"api/users/{userId}").WithOAuthBearerToken(token).DeleteAsync().ReceiveJson()
		);
	}

	private async Task<IdentityClientResponse> HandleRequestAsync<T>(
		string name,
		Func<string?, Task<T?>> request,
		bool @return = false
	)
	{
		try
		{
			var accessToken = await _accessTokenProvider.GetTokenAsync();
			var result = await request(accessToken);
			return new() { StatusCode = HttpStatusCode.OK, Content = @return ? result : null };
		}
		catch (FlurlHttpException ex)
		{
			ex.Call.Response.ThrowIfNull();
			await ProcessErrorAsync(ex, name);
			throw;
		}
	}

	private static IFlurlRequest Flurl(string appendPath) =>
		new Url(ExternalService.Ids.Issuer).AppendPathSegment(appendPath).WithTimeout(60);

	private async Task ProcessErrorAsync(FlurlHttpException ex, string action)
	{
		var response = await ex.Call.Response.GetStringAsync();
		var errContent = string.IsNullOrEmpty(response) ? "None" : response;
		var message = string.IsNullOrEmpty(ex.Message) ? "None" : ex.Message;

		var errMessageBuilder = new StringBuilder();
		errMessageBuilder.AppendLine($"Identity service exception when '{action.ToUpper()}' user:");
		errMessageBuilder.AppendLine($"Status: {ex.StatusCode} ({(HttpStatusCode)ex.Call.Response.StatusCode}");
		errMessageBuilder.AppendLine("Message:");
		errMessageBuilder.AppendLine(message);
		errMessageBuilder.AppendLine("Content:");
		errMessageBuilder.AppendLine(errContent);

		LoggerHelpers.Logger.Error(errMessageBuilder.ToString());
	}
}
