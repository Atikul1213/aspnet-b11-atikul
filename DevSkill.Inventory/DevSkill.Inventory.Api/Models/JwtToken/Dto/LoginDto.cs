using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Api.Models.JwtToken.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email or Username is required")]
        [Display(Name = "Email or UserName")]
        [EmailAddress]
        public string EmailOrUserName { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "ReCaptcha validation failed.")]
        public string ReCaptchaToken { get; set; } = default!;

    }
}
