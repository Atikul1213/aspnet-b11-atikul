using System.ComponentModel.DataAnnotations;

namespace Demo.Web.Areas.Admin.Models
{
    public class AddUserModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required, Compare("Password")]
        [Display(Name = "Comfirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
