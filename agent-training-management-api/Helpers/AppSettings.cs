namespace AtmAPI.Helpers;

public static class AppSettings
{
	public static ConfigurationManager Config { get; set; } = default!;

	public static string DbConnectionString => Config.GetConnectionString("atmdb");

	public static TValue GetValue<TValue>(string key) => Config.GetValue<TValue>(key);

	public static TValue GetSection<TValue>(string key) => Config.GetSection(key).Get<TValue>();

	public static IConfigurationSection GetSection(string key) => Config.GetSection(key);
}
