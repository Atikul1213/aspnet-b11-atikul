using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Models.IdentityModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
