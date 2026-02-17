using Cortex.Mediator.Commands;
using DevSkill.Core.Application;
using DevSkill.Inventory.Domain;

namespace DevSkill.Inventory.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, ResultResponse>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public DeleteProductCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<ResultResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _applicationUnitOfWork.ProductRepository.GetByIdAsync(command.Id);

            if (product is not null)
            {
                await _applicationUnitOfWork.ProductRepository.RemoveAsync(product);
                await _applicationUnitOfWork.SaveAsync();
            }
            return ResultResponse.Success(200, product);
        }
        #endregion
    }
}
