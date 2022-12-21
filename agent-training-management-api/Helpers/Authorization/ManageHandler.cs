namespace AtmAPI.Helpers.Authorization;

public class ManageHandler : AuthorizationHandler<ManageRequirement>
{
	private readonly string RequiredRole = "operator";
	private readonly string RequiredScope = "atm_api.full_access";

	protected override Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		ManageRequirement requirement
	)
	{
		if (!context.User.HasClaim(c => c.Type == "scope"))
			return Task.CompletedTask;

		var scopes = context.User.FindAll(c => c.Type == "scope");

		if (context.User.IsInRole(RequiredRole) && scopes.Any(s => s.Value == RequiredScope))
			context.Succeed(requirement);

		return Task.CompletedTask;
	}
}
