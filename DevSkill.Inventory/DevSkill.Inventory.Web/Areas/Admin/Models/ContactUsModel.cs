using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ContactUsModel
    {
        public Guid? Id { get; set; }
        [Required, MaxLength(128)]
        public string HeaderTitle { get; set; } = string.Empty;
        [Required, MaxLength(256)]
        public string HeaderSubtitle { get; set; } = string.Empty;
        [Required, MaxLength(512)]
        public string HeaderDescription { get; set; } = string.Empty;
        [Required, MaxLength(512)]
        public string Address { get; set; } = string.Empty;
        [Required, MaxLength(64)]
        public string Phone { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(128)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(128)]
        public string OpenHours { get; set; } = string.Empty;
        [Required, MaxLength(1024)]
        public string MapEmbedUrl { get; set; } = string.Empty;
    }
}
