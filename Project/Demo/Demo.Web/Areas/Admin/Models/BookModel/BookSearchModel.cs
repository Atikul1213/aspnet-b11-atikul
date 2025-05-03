namespace Demo.Web.Areas.Admin.Models.BookModel
{
    public class BookSearchModel
    {
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public DateTime? PublishDateFrom { get; set; }
        public DateTime? PublishDateTo { get; set; }
    }
}
