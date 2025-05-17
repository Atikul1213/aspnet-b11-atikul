using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Commands
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        public ProductUpdateCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            product.CreateOnUtc = DateTime.UtcNow;

            if (product is not null)
            {
                await _applicationUnitOfWork.ProductRepository.UpdateAsync(product);
                await _applicationUnitOfWork.SaveAsync();
            }
        }
    }
}
