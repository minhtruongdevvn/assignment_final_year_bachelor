using Microsoft.AspNetCore.Authorization;

namespace AAM.API.Authorization;

public class IdentityAuthorizationHandler : AuthorizationHandler<IdentityAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdentityAuthorizationRequirement requirement)
    {
        if (!(context.User.HasClaim(c => c.Type == "scope")))
            return Task.CompletedTask;

        var scopes = context.User.FindAll(c => c.Type == "scope");

        if (scopes.Any(s => s.Value == requirement.RequiredScope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

