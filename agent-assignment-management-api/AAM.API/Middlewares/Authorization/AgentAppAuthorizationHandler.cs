using Microsoft.AspNetCore.Authorization;

namespace AAM.API.Authorization;

public class AgentAppAuthorizationHandler : AuthorizationHandler<AgentAppAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgentAppAuthorizationRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "scope"))
            return Task.CompletedTask;

        var scopes = context.User.FindAll(c => c.Type == "scope");

        if (
            context.User.IsInRole(requirement.RequiredRole) && 
            scopes.Any(s => s.Value == requirement.RequiredScope)
        )
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

