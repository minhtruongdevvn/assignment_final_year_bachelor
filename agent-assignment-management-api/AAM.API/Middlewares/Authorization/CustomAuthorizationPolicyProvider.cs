using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AAM.API.Authorization;

public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
{
    private DefaultAuthorizationPolicyProvider DefaultPolicyProvider { get; }

    public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        DefaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => DefaultPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => DefaultPolicyProvider.GetFallbackPolicyAsync();

    public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy? policy;

        switch (policyName)
        {
            case AuthConstant.AppPolicy:
                policy = BuildAuthorizationPolicyFromRequirements(
                    new AppAuthorizationRequirement("aam_api.full_access", "operator", "AAM"));
                break;
            case AuthConstant.IdentityPolicy:
                policy = BuildAuthorizationPolicyFromRequirements(
                    new IdentityAuthorizationRequirement("IdentityServerApi"));
                break;
            case AuthConstant.AgentAppPolicy:
                policy = BuildAuthorizationPolicyFromRequirements(
                    new AgentAppAuthorizationRequirement("aam_api.agent", "agent"));
                break;
            default:
                policy = await DefaultPolicyProvider.GetPolicyAsync(policyName);
                break;
        }

        return policy;
    }

    private AuthorizationPolicy? BuildAuthorizationPolicyFromRequirements(params IAuthorizationRequirement[] requirements)
    {
        var policy = new AuthorizationPolicyBuilder();
        policy.AddRequirements(requirements);
        return policy.Build();
    }
}
