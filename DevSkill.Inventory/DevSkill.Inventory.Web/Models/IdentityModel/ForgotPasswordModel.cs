using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Models.IdentityModel
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Enter a valid Email.")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
