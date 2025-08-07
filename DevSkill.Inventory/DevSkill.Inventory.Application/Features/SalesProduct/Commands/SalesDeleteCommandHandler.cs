using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Commands
{
    public class SalesDeleteCommandHandler : IRequestHandler<SalesDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public SalesDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task Handle(SalesDeleteCommand request, CancellationToken cancellationToken)
        {
            var sales = await _applicationUnitOfWork.SalesRepository.GetByIdAsync(request.Id);

            if (sales is not null)
            {
                await _applicationUnitOfWork.SalesRepository.RemoveAsync(sales);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
