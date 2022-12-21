namespace AgentIdentityServer.Pages.Error;

[AllowAnonymous]
[SecurityHeaders]
public class Index : PageModel
{
	private readonly IWebHostEnvironment _environment;
	private readonly IIdentityServerInteractionService _interaction;

	public Index(IIdentityServerInteractionService interaction, IWebHostEnvironment environment)
	{
		_interaction = interaction;
		_environment = environment;
	}

	public ViewModel? View { get; set; }

	public async Task OnGet(string errorId)
	{
		View = new ViewModel();

		// retrieve error details from IdentityServer
		var message = await _interaction.GetErrorContextAsync(errorId);
		if (message != null)
		{
			View.Error = message;

			if (!_environment.IsDevelopment())
				// only show in development
				message.ErrorDescription = null;
		}
	}
}
