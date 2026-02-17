using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Models
{
    public class ContactUsInfoModel
    {
        public string HeaderTitle { get; set; } = "Contact Us";
        public string HeaderSubtitle { get; set; } = "We're here to help you secceed";
        public string HeaderDescription { get; set; } = "Any question or remark? Just write us a message.";
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string OpenHours { get; set; } = string.Empty;
        public string MapEmbedUrl { get; set; } = string.Empty;

        // Form fields
        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public string? Name { get; set; }

        [RegularExpression(@"^(?:\+88)?01[3-9]\d{8}$", ErrorMessage = "Please enter a valid Bangladeshi phone number.")]
        [StringLength(14, ErrorMessage = "Phone number must be less than 14 characters.")]
        public string? UserPhone { get; set; }

        [Required(ErrorMessage = "The Email field is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage = "The Message field is required.")]
        [StringLength(2000, ErrorMessage = "Message must be less than 2000 characters.")]
        public string? Message { get; set; }
    }
}
