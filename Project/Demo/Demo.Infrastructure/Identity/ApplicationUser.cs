using Microsoft.AspNetCore.Identity;

namespace Demo.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
