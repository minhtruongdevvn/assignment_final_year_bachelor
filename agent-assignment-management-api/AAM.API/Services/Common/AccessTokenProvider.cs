using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace AAM.API;

public class AccessTokenProvider
{
    private readonly IConfiguration _configuration;
    private readonly string _accessTokenKey;
    private readonly IMemoryCache _memoryCache;

    public AccessTokenProvider(IMemoryCache memoryCache, IConfiguration configuration)
    {
        _configuration = configuration;
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

    public string GetIssuer() =>
        _configuration.GetValue<string>("Authentication:IDS_ISSUER");

    private async Task<TokenResponse> UntestedProcessTokenAsync()
    {
        var url = new Url(GetIssuer())
            .AppendPathSegment("connect/token")
            .WithTimeout(60)
            .WithBasicAuth(
            _configuration.GetValue<string>("Authentication:IDS_CLIENT_ID"), 
            _configuration.GetValue<string>("Authentication:IDS_CLIENT_SECRET")
        );

        // request with type of x-www-form-urlencoded
        var token = await url.PostUrlEncodedAsync(new { grant_type = "client_credentials" })
            .ReceiveJson<TokenResponse>();

        return token!;
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