using AutoMapper;
using DevSkill.Inventory.Application.Exceptions;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Commands
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public ProductUpdateCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            product.CreateOnUtc = DateTime.UtcNow;

            bool isDuplicate = await _applicationUnitOfWork.ProductRepository.CheckSkuDuplicateAsync(product.Sku, product.Id);

            if (!isDuplicate)
            {
                await _applicationUnitOfWork.ProductRepository.UpdateAsync(product);
                await _applicationUnitOfWork.SaveAsync();
            }
            else
            {
                throw new DuplicateProductSkuException();
            }
        }

        #endregion
    }
}
