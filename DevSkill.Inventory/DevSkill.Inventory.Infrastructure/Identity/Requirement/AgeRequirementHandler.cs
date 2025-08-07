using Microsoft.AspNetCore.Authorization;

namespace DevSkill.Inventory.Infrastructure.Identity.Requirement
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "age" &&
            int.Parse(c.Value) >= 20 && int.Parse(c.Value) <= 40))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
