using System.Data;

namespace AgentIdentityServer.Helpers;

public static class EnvVars
{
	public static bool IsDev => GetEnvVar("ASPNETCORE_ENVIRONMENT") == "Development";

	private static string GetEnvVar(string variable)
	{
		return Environment
			.GetEnvironmentVariable(variable)
			.ThrowIfNull($"Environment variable \"{variable}\" not found");
	}

	public static class Ids
	{
		public static class Clients
		{
			public static class INTERNAL_API
			{
				public static string Id => GetEnvVar("INTERNAL_CLIENT_ID_M2M");
				public static string Secret => GetEnvVar("INTERNAL_CLIENT_SECRET_ID_M2M");
			}

			public static class ATM_SPA
			{
				public static string Id => GetEnvVar("IDS_CLIENT_ID_ATM_SPA");
				public static string Secret => GetEnvVar("IDS_CLIENT_SECRET_ATM_SPA");
			}

			public static class AAM_APP
			{
				public static string Id => GetEnvVar("IDS_CLIENT_ID_AAM_APP");
				public static string Secret => GetEnvVar("IDS_CLIENT_SECRET_AAM_APP");
			}

			public static class AGENT_APP
			{
				public static string Id => GetEnvVar("IDS_CLIENT_ID_AGENT_APP");
				public static string Secret => GetEnvVar("IDS_CLIENT_SECRET_AGENT_APP");
			}
		}

		public static class HostUrls
		{
			public static string AtmSpa => GetEnvVar("HOST_URL_ATM_SPA");
		}
	}
}
