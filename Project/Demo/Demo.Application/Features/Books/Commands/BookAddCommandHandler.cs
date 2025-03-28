using Demo.Domain;
using Demo.Domain.Entities;
using MediatR;

namespace Demo.Application.Features.Books.Commands
{
    public class BookAddCommandHandler : IRequestHandler<BookAddCommand>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public BookAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public async Task Handle(BookAddCommand request, CancellationToken cancellationToken)
        {
            await _applicationUnitOfWork.BookRepository.AddAsync(new Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId
            });

            await _applicationUnitOfWork.SaveAsync();
        }
    }
}
