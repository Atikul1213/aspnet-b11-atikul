using DevSkill.Inventory.Domain.Templates;

namespace DevSkill.Inventory.Infrastructure.Templates
{
    public partial class PasswordResetEmailTemplate : IPasswordResetEmailTemplate
    {
        public string? UserName { get; set; }
        public string? CallbackUrl { get; set; }
    }
}
