using Demo.Domain;

namespace Demo.Web.Areas.Admin.Models.BookModel
{
    public class BookListModel : DataTables
    {
        public BookSearchModel SearchItem { get; set; }
    }
}
