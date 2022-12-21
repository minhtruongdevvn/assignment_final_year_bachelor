namespace AgentIdentityServer.Pages.Diagnostics;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
	public ViewModel? View { get; set; }

	public async Task<IActionResult> OnGet()
	{
		var localAddresses = new[]
		{
			"127.0.0.1",
			"::1",
			HttpContext.Connection.LocalIpAddress?.ToString()
		};
		if (localAddresses.Contains(HttpContext.Connection.RemoteIpAddress?.ToString()))
			View = new ViewModel(await HttpContext.AuthenticateAsync());

		return !localAddresses.Contains(HttpContext.Connection.RemoteIpAddress?.ToString())
			? NotFound()
			: Page();
	}
}
