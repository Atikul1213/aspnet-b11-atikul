using DevSkill.Core.Infrastructure.Features.Membership;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevSkill.Inventory.Infrastructure.Identity
{
    public class ApplicationUser : SystemUser
    {
        public DateTime? LastLogin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public IList<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
