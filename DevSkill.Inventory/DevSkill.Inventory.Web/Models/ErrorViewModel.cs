namespace DevSkill.Inventory.Web.Models
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }
        public string Title { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
        public string? TraceId { get; set; }
    }
}
