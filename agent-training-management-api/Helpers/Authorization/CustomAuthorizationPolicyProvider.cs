namespace AtmAPI.Helpers.Authorization;

public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
{
	private DefaultAuthorizationPolicyProvider DefaultPolicyProvider { get; }

	public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) =>
		DefaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);

	public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
		DefaultPolicyProvider.GetDefaultPolicyAsync();

	public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
		DefaultPolicyProvider.GetFallbackPolicyAsync();

	public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
	{
		var policyAndArgument = policyName.Split('.');
		var basePolicy = policyAndArgument[0];
		var policy = basePolicy switch
		{
			AuthConstant.RolePolicy
				=> BuildAuthorizationPolicyFromRequirements(
					new RoleRequirement(policyAndArgument[1].Split(','))
				),
			AuthConstant.ManagePolicy
				=> BuildAuthorizationPolicyFromRequirements(new ManageRequirement()),
			_ => await DefaultPolicyProvider.GetPolicyAsync(policyName),
		};
		return policy;
	}

	private static AuthorizationPolicy? BuildAuthorizationPolicyFromRequirements(
		params IAuthorizationRequirement[] requirements
	)
	{
		var policy = new AuthorizationPolicyBuilder();
		policy.AddRequirements(requirements);

		return policy.Build();
	}
}
