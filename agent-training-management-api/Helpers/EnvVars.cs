namespace AtmAPI.Helpers;

public static class EnvVars
{
	public static bool IsDev => GetEnvVar("ASPNETCORE_ENVIRONMENT") == "Development";

	private static string GetEnvVar(string variable)
	{
		return Environment
			.GetEnvironmentVariable(variable)
			.ThrowIfNull($"Environment variable \"{variable}\" not found");
	}

	public static class ExternalService
	{
		public static class Ids
		{
			public static string Issuer => GetEnvVar("IDS_ISSUER");
			public static string ClientId => GetEnvVar("IDS_CLIENT_ID");
			public static string ClientSecret => GetEnvVar("IDS_CLIENT_SECRET");
			public static string GrantType => GetEnvVar("IDS_GRANT_TYPE");
		}

		public static class Assignment
		{
			public static string Endpoint => GetEnvVar("ASSIGNMENT_ENDPOINT");
		}
	}
}
