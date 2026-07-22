using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Models.IdentityModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email or Username is required")]
        [Display(Name = "Email or UserName")]
        [EmailAddress]
        public string EmailOrUserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } = false;
        public string? ReturnUrl { get; set; }
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
