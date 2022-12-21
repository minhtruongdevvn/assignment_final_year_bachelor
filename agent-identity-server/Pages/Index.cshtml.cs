using Duende.IdentityServer.Hosting;

namespace AgentIdentityServer.Pages;

[AllowAnonymous]
public class Index : PageModel
{
	public string? Version;

	public void OnGet()
	{
		Version = typeof(IdentityServerMiddleware).Assembly
			.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
			?.InformationalVersion.Split('+')
			.First();
	}
}
