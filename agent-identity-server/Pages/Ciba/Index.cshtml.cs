// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace AgentIdentityServer.Pages.Ciba;

[AllowAnonymous]
[SecurityHeaders]
public class IndexModel : PageModel
{
	private readonly IBackchannelAuthenticationInteractionService _backchannelAuthenticationInteraction;
	private readonly ILogger<IndexModel> _logger;

	public IndexModel(
		IBackchannelAuthenticationInteractionService backchannelAuthenticationInteractionService,
		ILogger<IndexModel> logger
	)
	{
		_backchannelAuthenticationInteraction = backchannelAuthenticationInteractionService;
		_logger = logger;
	}

	public BackchannelUserLoginRequest? LoginRequest { get; set; }

	public async Task<IActionResult> OnGet(string id)
	{
		LoginRequest = await _backchannelAuthenticationInteraction.GetLoginRequestByInternalIdAsync(
			id
		);
		if (LoginRequest == null)
			_logger.Warn($"Invalid backchannel login id {id}");

		return LoginRequest == null ? RedirectToPage("/Home/Error/Index") : Page();
	}
}
