namespace DevSkill.Inventory.Domain.Templates
{
    public interface IPasswordChangeEmailTemplate
    {
        public string? UserName { get; set; }
        public string? DateValue { get; set; }
        public string TransformText();
    }
}
