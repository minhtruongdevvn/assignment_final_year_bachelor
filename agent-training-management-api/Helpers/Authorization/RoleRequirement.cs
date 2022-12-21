namespace AtmAPI.Helpers.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
	public string[] RequiredRoles { get; }

	public RoleRequirement(string[] requireRoles) => RequiredRoles = requireRoles;
}

public class RoleAuthorization : AuthorizeAttribute
{
	public RoleAuthorization(params string[] roles) =>
		Policy = $"{AuthConstant.RolePolicy}.{string.Join(',', roles)}";
}
