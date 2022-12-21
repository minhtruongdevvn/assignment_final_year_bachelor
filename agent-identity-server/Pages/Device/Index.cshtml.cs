using AgentIdentityServer.Pages.Consent;
using Duende.IdentityServer.Validation;

namespace AgentIdentityServer.Pages.Device;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
	private readonly IEventService _events;
	private readonly IDeviceFlowInteractionService _interaction;

	public Index(IDeviceFlowInteractionService interaction, IEventService eventService)
	{
		_interaction = interaction;
		_events = eventService;
	}

	public ViewModel? View { get; set; }

	[BindProperty] public InputModel? Input { get; set; }

	public async Task<IActionResult> OnGet(string userCode)
	{
		if (string.IsNullOrWhiteSpace(userCode))
		{
			View = new ViewModel();
			Input = new InputModel();
			return Page();
		}

		View = await BuildViewModelAsync(userCode);
		if (View == null)
		{
			ModelState.AddModelError("", DeviceOptions.InvalidUserCode);
			View = new ViewModel();
			Input = new InputModel();
			return Page();
		}

		Input = new InputModel { UserCode = userCode };

		return Page();
	}

	public async Task<IActionResult> OnPost()
	{
		var request = await _interaction.GetAuthorizationContextAsync(Input?.UserCode);
		if (request == null)
			return RedirectToPage("/Home/Error/Index");

		ConsentResponse? grantedConsent = null;

		// user clicked 'no' - send back the standard 'access_denied' response
		if (Input?.Button == "no")
		{
			grantedConsent = new ConsentResponse { Error = AuthorizationError.AccessDenied };

			// emit event
			await _events.RaiseAsync(
				new ConsentDeniedEvent(
					User.GetSubjectId(),
					request.Client.ClientId,
					request.ValidatedResources.RawScopeValues
				)
			);
		}
		// user clicked 'yes' - validate the data
		else if (Input?.Button == "yes")
		{
			// if the user consented to some scope, build the response model
			if (Input.ScopesConsented != null && Input.ScopesConsented.Any())
			{
				grantedConsent = new ConsentResponse
				{
					Description = Input?.Description,
					RememberConsent = Input?.RememberConsent ?? true,
					ScopesValuesConsented = Input
						?.ScopesConsented?.Where(x => x != StandardScopes.OfflineAccess)
						.ToArray()
				};

				// emit event
				await _events.RaiseAsync(
					new ConsentGrantedEvent(
						User.GetSubjectId(),
						request.Client.ClientId,
						request.ValidatedResources.RawScopeValues,
						grantedConsent.ScopesValuesConsented,
						grantedConsent.RememberConsent
					)
				);
			}
			else
			{
				ModelState.AddModelError("", ConsentOptions.MustChooseOneErrorMessage);
			}
		}
		else
		{
			ModelState.AddModelError("", ConsentOptions.InvalidSelectionErrorMessage);
		}

		if (grantedConsent != null)
		{
			// communicate outcome of consent back to IdentityServer
			await _interaction.HandleRequestAsync(Input?.UserCode, grantedConsent);

			// indicate that's it ok to redirect back to authorization endpoint
			return RedirectToPage("/Device/Success");
		}

		// we need to redisplay the consent UI
		View = await BuildViewModelAsync(Input?.UserCode!, Input);
		return Page();
	}

	private async Task<ViewModel?> BuildViewModelAsync(string userCode, InputModel? model = null)
	{
		var request = await _interaction.GetAuthorizationContextAsync(userCode);
		return request != null ? CreateConsentViewModel(model, request) : null;
	}

	private ViewModel? CreateConsentViewModel(
		InputModel? model,
		DeviceFlowAuthorizationRequest request
	)
	{
		var vm = new ViewModel
		{
			ClientName = request.Client.ClientName ?? request.Client.ClientId,
			ClientUrl = request.Client.ClientUri,
			ClientLogoUrl = request.Client.LogoUri,
			AllowRememberConsent = request.Client.AllowRememberConsent,
			IdentityScopes = request.ValidatedResources.Resources.IdentityResources
				.Select(
					x =>
						CreateScopeViewModel(
							x,
							model == null || model.ScopesConsented?.Contains(x.Name) == true
						)
				)
				.ToArray()
		};

		var apiScopes = new List<ScopeViewModel>();
		foreach (var parsedScope in request.ValidatedResources.ParsedScopes)
		{
			var apiScope = request.ValidatedResources.Resources.FindApiScope(
				parsedScope.ParsedName
			);
			if (apiScope != null)
			{
				var scopeVm = CreateScopeViewModel(
					parsedScope,
					apiScope,
					model == null || model.ScopesConsented?.Contains(parsedScope.RawValue) == true
				);
				apiScopes.Add(scopeVm);
			}
		}

		if (DeviceOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
			apiScopes.Add(
				GetOfflineAccessScope(
					model == null
					|| model.ScopesConsented?.Contains(StandardScopes.OfflineAccess) == true
				)
			);
		vm.ApiScopes = apiScopes;

		return vm;
	}

	private static ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
	{
		return new ScopeViewModel
		{
			Value = identity.Name,
			DisplayName = identity.DisplayName ?? identity.Name,
			Description = identity.Description,
			Emphasize = identity.Emphasize,
			Required = identity.Required,
			Checked = check || identity.Required
		};
	}

	public ScopeViewModel CreateScopeViewModel(
		ParsedScopeValue parsedScopeValue,
		ApiScope apiScope,
		bool check
	)
	{
		return new ScopeViewModel
		{
			Value = parsedScopeValue.RawValue,
			// todo: use the parsed scope value in the display?
			DisplayName = apiScope.DisplayName ?? apiScope.Name,
			Description = apiScope.Description,
			Emphasize = apiScope.Emphasize,
			Required = apiScope.Required,
			Checked = check || apiScope.Required
		};
	}

	private static ScopeViewModel GetOfflineAccessScope(bool check)
	{
		return new ScopeViewModel
		{
			Value = StandardScopes.OfflineAccess,
			DisplayName = DeviceOptions.OfflineAccessDisplayName,
			Description = DeviceOptions.OfflineAccessDescription,
			Emphasize = true,
			Checked = check
		};
	}
}
