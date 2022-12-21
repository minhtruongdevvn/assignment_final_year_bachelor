namespace AgentIdentityServer;

public static class Config
{
	public static IEnumerable<IdentityResource> IdentityResources =>
		new List<IdentityResource> { new IdentityResources.OpenId(), };

	public static IEnumerable<ApiScope> ApiScopes =>
		new ApiScope[]
		{
			new(LocalApi.ScopeName),
			new("atm_api.full_access"),
			new("aam_api.full_access"),
			new("aam_api.agent"),
		};

	public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>();

	public static IEnumerable<Client> Clients =>
		new Client[]
		{
			// Internal machine Client
			new()
			{
				ClientId = Ids.Clients.INTERNAL_API.Id,
				ClientSecrets = { new Secret(Ids.Clients.INTERNAL_API.Secret.Sha256()) },
				AllowedGrantTypes = GrantTypes.ClientCredentials,
				AccessTokenLifetime = (int)TimeSpan.FromMinutes(25).TotalSeconds,
				AllowedScopes = { LocalApi.ScopeName }
			},
			// ATM SPA Client
			new()
			{
				ClientId = Ids.Clients.ATM_SPA.Id,
				ClientName = "SPA: Agent Training Management",
				RequireClientSecret = false,
				RequirePkce = true,
				AllowedGrantTypes = GrantTypes.Code,
				AllowAccessTokensViaBrowser = true,
				UserSsoLifetime = (int)TimeSpan.FromDays(1).TotalSeconds,
				AccessTokenLifetime = (int)TimeSpan.FromMinutes(25).TotalSeconds,
				RefreshTokenExpiration = TokenExpiration.Absolute,
				SlidingRefreshTokenLifetime = 0,
				AllowOfflineAccess = true,
				CoordinateLifetimeWithUserSession = true,
				AllowedCorsOrigins = { Ids.HostUrls.AtmSpa },
				ClientUri = Ids.HostUrls.AtmSpa,
				PostLogoutRedirectUris = { Ids.HostUrls.AtmSpa },
				RedirectUris =
				{
					Ids.HostUrls.AtmSpa,
					Ids.HostUrls.AtmSpa + "/callback",
					Ids.HostUrls.AtmSpa + "/silent-renew"
				},
				AllowedScopes = { StandardScopes.OpenId, "atm_api.full_access", }
			},
			// AAM APP Client
			new()
			{
				// client config
				ClientId = Ids.Clients.AAM_APP.Id,
				ClientSecrets = { new Secret(Ids.Clients.AAM_APP.Secret.Sha256()) },
				ClientName = "Agent Assignment Management",
				RequireClientSecret = true,
				AllowedScopes =
				{
					StandardScopes.OpenId,
					StandardScopes.OfflineAccess,
					"aam_api.full_access"
				},
				// credential flow & logic
				AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
				AccessTokenLifetime = (int)TimeSpan.FromHours(1).TotalSeconds,
				UserSsoLifetime = (int)TimeSpan.FromDays(1).TotalSeconds,
				// refresh token logic
				AllowOfflineAccess = true,
				AbsoluteRefreshTokenLifetime = (int)TimeSpan.FromDays(1).TotalSeconds,
				SlidingRefreshTokenLifetime = (int)TimeSpan.FromHours(1).TotalSeconds,
				RefreshTokenUsage = TokenUsage.OneTimeOnly,
				RefreshTokenExpiration = TokenExpiration.Sliding,
				UpdateAccessTokenClaimsOnRefresh = false
			},
			// AGENT APP Client
			new()
			{
				// client config
				ClientId = Ids.Clients.AGENT_APP.Id,
				ClientSecrets = { new Secret(Ids.Clients.AGENT_APP.Secret.Sha256()) },
				ClientName = "Agent App",
				RequireClientSecret = true,
				// credential flow & logic
				AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
				AccessTokenLifetime = (int)TimeSpan.FromHours(1).TotalSeconds,
				UserSsoLifetime = (int)TimeSpan.FromDays(1).TotalSeconds,
				// refresh token logic
				AllowOfflineAccess = true,
				AbsoluteRefreshTokenLifetime = (int)TimeSpan.FromDays(30).TotalSeconds,
				RefreshTokenUsage = TokenUsage.ReUse,
				RefreshTokenExpiration = TokenExpiration.Absolute,
				UpdateAccessTokenClaimsOnRefresh = false,
				AllowedScopes =
				{
					StandardScopes.OpenId,
					StandardScopes.OfflineAccess,
					"aam_api.agent"
				},
			},
		};
}
