using Microsoft.AspNetCore.Authorization;

namespace DevSkill.Inventory.Infrastructure.Identity.Requirement
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string requiredRole)
        {
            RequiredRole = requiredRole;
        }
        public string RequiredRole { get; }
    }
}
