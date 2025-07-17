using Microsoft.AspNetCore.Identity;

namespace DevSkill.Inventory.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public int StatusId { get; set; }
        public int CompanyId { get; set; }
    }
}
