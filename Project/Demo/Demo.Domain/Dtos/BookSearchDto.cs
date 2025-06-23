namespace Demo.Domain.Dtos
{
    public class BookSearchDto
    {
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? PublishDateFrom { get; set; }
        public DateTime? PublishDateTo { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
    }
}
