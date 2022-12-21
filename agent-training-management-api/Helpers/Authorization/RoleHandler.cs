namespace AtmAPI.Helpers.Authorization;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
	protected override Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		RoleRequirement requirement
	)
	{
		if (requirement.RequiredRoles.Any(r => context.User.IsInRole(r)))
			context.Succeed(requirement);

		return Task.CompletedTask;
	}
}
