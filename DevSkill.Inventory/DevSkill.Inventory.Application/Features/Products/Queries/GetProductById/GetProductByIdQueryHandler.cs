using Cortex.Mediator.Queries;
using DevSkill.Core.Application;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ResultResponse<Product>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetProductByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<ResultResponse<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _applicationUnitOfWork.ProductRepository.GetByIdAsync(query.Id);

            return ResultResponse.Success(200, product);
        }
        #endregion
    }
}
