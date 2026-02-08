namespace DevSkill.Inventory.Web.Models
{
    public class UserInfoModel
    {
        public bool IsAuthenticated { get; set; } = false;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicturePath { get; set; }
    }
}
