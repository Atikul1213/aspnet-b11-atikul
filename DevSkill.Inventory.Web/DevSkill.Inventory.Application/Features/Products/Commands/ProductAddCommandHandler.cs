using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Commands
{
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public ProductAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                IsAvailable = request.IsAvailable,
                CreateOnUtc = DateTime.UtcNow
            };

            await _applicationUnitOfWork.ProductRepository.AddAsync(product);
            await _applicationUnitOfWork.SaveAsync();
        }
    }
}
