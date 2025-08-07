namespace DevSkill.Inventory.Web.Areas.Admin.Models.Category
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CreateOnUtc { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
    }
}
