using Demo.Domain;
using Demo.Domain.Entities;
using MediatR;

namespace Demo.Application.Features.Books.Queries
{
    public class GetBookQueryHandler : IRequestHandler<GetBooksQuery, (IList<Book>, int, int)>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public GetBookQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public async Task<(IList<Book>, int, int)> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.BookRepository.GetPagedBooksAsync(request);
        }
    }
}
