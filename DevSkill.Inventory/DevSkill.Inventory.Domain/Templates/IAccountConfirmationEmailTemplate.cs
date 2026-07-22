namespace DevSkill.Inventory.Domain.Templates
{
    public interface IAccountConfirmationEmailTemplate
    {
        public string? UserName { get; set; }
        public string? CallbackUrl { get; set; }
        public string TransformText();
    }
}
