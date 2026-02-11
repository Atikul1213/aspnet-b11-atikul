namespace DevSkill.Inventory.Web.Models.IdentityModel
{
    public class ConfirmEmailModel
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string? email { get; set; }
        public string? returnUrl { get; set; }
    }
}
