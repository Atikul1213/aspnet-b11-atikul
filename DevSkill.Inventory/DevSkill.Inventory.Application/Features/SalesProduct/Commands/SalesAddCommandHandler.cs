using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Commands
{
    public class SalesAddCommandHandler : IRequestHandler<SalesAddCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public SalesAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(SalesAddCommand request, CancellationToken cancellationToken)
        {
            var sales = _mapper.Map<Sales>(request);

            await _applicationUnitOfWork.SalesRepository.AddAsync(sales);
            await _applicationUnitOfWork.SaveAsync();

        }

        #endregion
    }
}
