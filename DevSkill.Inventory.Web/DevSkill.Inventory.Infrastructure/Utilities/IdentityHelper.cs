using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace DevSkill.Inventory.Infrastructure.Utilities
{
    public static class IdentityHelper
    {
        public static ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        public static IUserEmailStore<ApplicationUser> GetEmailStore(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
        {
            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)userStore;
        }
    }
}
