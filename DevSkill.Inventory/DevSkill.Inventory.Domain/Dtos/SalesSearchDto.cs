namespace DevSkill.Inventory.Domain.Dtos
{
    public class SalesSearchDto
    {
        public string? CustomerName { get; set; }
        public int? StatusId { get; set; }
        public int? TotalFrom { get; set; }
        public int? TotalTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
