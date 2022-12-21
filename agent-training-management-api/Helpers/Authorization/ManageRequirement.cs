namespace AtmAPI.Helpers.Authorization;

public class ManageRequirement : IAuthorizationRequirement
{
	public ManageRequirement() { }
}

public class ManageAuthorization : AuthorizeAttribute
{
	public ManageAuthorization() => Policy = AuthConstant.ManagePolicy;
}
