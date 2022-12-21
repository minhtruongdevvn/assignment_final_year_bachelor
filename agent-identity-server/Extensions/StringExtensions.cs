using Newtonsoft.Json.Serialization;

namespace AgentIdentityServer.Extensions;

public static class StringExtensions
{
	public static string ToSnakeCase(this string? str) =>
		str != null
			? new DefaultContractResolver
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			}.GetResolvedPropertyName(str)
			: "";
}
