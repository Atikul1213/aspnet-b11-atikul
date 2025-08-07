using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Commands
{
    public class UnitAddCommandHandler : IRequestHandler<UnitAddCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UnitAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task Handle(UnitAddCommand request, CancellationToken cancellationToken)
        {
            var productUnit = _mapper.Map<ProductUnit>(request);
            productUnit.CreateOnUtc = DateTime.UtcNow;

            await _applicationUnitOfWork.ProductUnitRepository.AddAsync(productUnit);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
