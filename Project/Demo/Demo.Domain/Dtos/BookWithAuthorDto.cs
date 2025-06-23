namespace Demo.Domain.Dtos
{
    public class BookWithAuthorDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishDate { get; set; }
        public double Price { get; set; }
    }
}
