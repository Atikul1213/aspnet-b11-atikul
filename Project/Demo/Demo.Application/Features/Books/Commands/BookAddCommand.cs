using MediatR;

namespace Demo.Application.Features.Books.Commands
{
    public class BookAddCommand : IRequest
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
    }
}
