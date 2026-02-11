namespace DevSkill.Inventory.Domain.Templates
{
    public interface IPasswordResetEmailTemplate
    {
        public string? UserName { get; set; }
        public string? CallBackUrl { get; set; }
        public string TransformText();
    }
}
