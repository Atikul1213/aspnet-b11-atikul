using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DevSkill.Inventory.Infrastructure.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store,
           IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
           ILookupNormalizer lookupNormalizer, IdentityErrorDescriber errors,
           ILogger<RoleManager<ApplicationRole>> logger)
           : base(store, roleValidators, lookupNormalizer, errors, logger)
        {

        }
    }
}
