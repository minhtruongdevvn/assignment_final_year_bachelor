using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;

namespace AtmAPI.Services;

public class AccessTokenProvider
{
	private readonly string _accessTokenKey;
	private readonly IMemoryCache _memoryCache;

	public AccessTokenProvider(IMemoryCache memoryCache)
	{
		_accessTokenKey = "access_token";
		_memoryCache = memoryCache;
	}

	public async Task<string?> GetTokenAsync()
	{
		var token = await _memoryCache.GetOrCreateAsync(
			_accessTokenKey,
			async entry =>
			{
				var tokenResponse = await UntestedProcessTokenAsync();
				entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(
					tokenResponse!.Expired - 300
				);
				return tokenResponse;
			}
		);
		return token.AccessToken;
	}

	private static async Task<TokenResponse> UntestedProcessTokenAsync()
	{
		var url = new Url(ExternalService.Ids.Issuer)
			.AppendPathSegment("connect/token")
			.WithTimeout(60)
			.WithBasicAuth(ExternalService.Ids.ClientId, ExternalService.Ids.ClientSecret);

		// request with type of x-www-form-urlencoded
		var token = await url.PostUrlEncodedAsync(new { grant_type = "client_credentials" })
			.ReceiveJson<TokenResponse>();

		token.ThrowIfNull();

		return token;
	}

	private class TokenResponse
	{
		[JsonProperty("access_token")]
		public string? AccessToken { get; set; }

		[JsonProperty("expires_in")]
		public int Expired { get; set; } = 3600;

		[JsonProperty("scope")]
		public string? Scope { get; set; }

		[JsonProperty("token_type")]
		public string? TokenType { get; set; }
	}
}
