using Microsoft.AspNetCore.Authorization;

namespace DevSkill.Inventory.Infrastructure.Identity.Requirement
{
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (context.User.IsInRole(requirement.RequiredRole))
                context.Succeed(requirement);

            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
