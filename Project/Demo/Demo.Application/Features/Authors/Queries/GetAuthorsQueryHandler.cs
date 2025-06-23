using Demo.Domain;
using Demo.Domain.Entities;
using MediatR;

namespace Demo.Application.Features.Authors.Queries
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IList<Author>>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public GetAuthorsQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public async Task<IList<Author>> Handle(GetAuthorsQuery request,
           CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.AuthorRepository.GetAllAsync();
        }
    }
}
