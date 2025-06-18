using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Commands
{
    public class UnitDeleteCommandHandler : IRequestHandler<UnitDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public UnitDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods

        public async Task Handle(UnitDeleteCommand request, CancellationToken cancellationToken)
        {
            var productUnit = await _applicationUnitOfWork.ProductUnitRepository.GetByIdAsync(request.Id);

            if (productUnit != null)
            {
                await _applicationUnitOfWork.ProductUnitRepository.RemoveAsync(productUnit);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
