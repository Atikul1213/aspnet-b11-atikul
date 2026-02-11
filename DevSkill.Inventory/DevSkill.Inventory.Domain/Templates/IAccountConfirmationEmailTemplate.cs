namespace DevSkill.Inventory.Domain.Templates
{
    public interface IAccountConfirmationEmailTemplate
    {
        public string? UserName { get; set; }
        public string? CallBackUrl { get; set; }
        public string TransformTest();
    }
}
