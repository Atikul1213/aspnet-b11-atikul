namespace Demo.Web.Areas.Admin.Models.BookModel
{
    public class AddBookModel
    {
        public string Title { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
