using DevSkill.Inventory.Domain.Templates;

namespace DevSkill.Inventory.Infrastructure.Templates
{
    public partial class PasswordChangeEmailTemplate : IPasswordChangeEmailTemplate
    {
        public string? UserName { get; set; }
        public string? DateValue { get; set; }
    }
}
