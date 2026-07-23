using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Models.IdentityModel
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; } = default!;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation do not match.")]
        public string? ConfirmPassword { get; set; }
        public string? Area { get; set; }
        public string? StatusMessage { get; set; }
    }
}
