using Duende.IdentityServer.Validation;

namespace AgentIdentityServer.Middlewares;

public class CustomTokenRequestValidator : ICustomTokenRequestValidator
{
	private readonly UserManager<AppUser> _userManager;

	public CustomTokenRequestValidator(UserManager<AppUser> userManager) =>
		_userManager = userManager;

	public async Task ValidateAsync(CustomTokenRequestValidationContext context)
	{
		var gt = context.Result.ValidatedRequest.GrantType;
		if (gt is GrantType.ResourceOwnerPassword or GrantType.AuthorizationCode)
		{
			var sub =
				context.Result.ValidatedRequest.Subject.Claims
					.FirstOrDefault(x => x.Type == "sub")
					?.Value ?? null;

			string? role = null,
				givenName = null,
				familyName = null,
				internalCode = null,
				belongTo = null;

			if (!string.IsNullOrEmpty(sub))
			{
				var user = await _userManager.FindByIdAsync(sub);
				internalCode = user?.InternalCode;
				familyName = user?.FamilyName;
				givenName = user?.GivenName;
				belongTo = user?.BelongTo;

				var roles = user == null ? null : await _userManager.GetRolesAsync(user);
				role = roles?.FirstOrDefault();
			}

			context.Result.CustomResponse = new()
			{
				{ "user_id", sub },
				{ "family_name", familyName },
				{ "internal_code", internalCode },
				{ "given_name", givenName },
				{ "belong_to", belongTo },
				{ "role", role },
			};
		}
	}
}
