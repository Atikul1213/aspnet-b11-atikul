using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, (IList<Product>, int, int)>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public GetProductQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public async Task<(IList<Product>, int, int)> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.ProductRepository.GetCQRSPagedProductAsync(request);
        }
    }
}
