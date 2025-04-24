using Demo.Infrastructure.Identity.Requirement;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Infrastructure.Extensions
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AgeRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "age" &&
                int.Parse(x.Value) > 20 && int.Parse(x.Value) < 40))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
