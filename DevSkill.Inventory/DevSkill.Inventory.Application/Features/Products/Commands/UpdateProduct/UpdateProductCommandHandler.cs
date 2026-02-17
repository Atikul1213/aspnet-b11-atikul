using AutoMapper;
using Cortex.Mediator.Commands;
using DevSkill.Core.Application;
using DevSkill.Inventory.Application.Exceptions;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, ResultResponse>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UpdateProductCommandHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<ResultResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Product>(command);
                bool isDuplicate = await _applicationUnitOfWork.ProductRepository.CheckBarCodeDuplicateAsync(product.BarCode);

                if (!isDuplicate)
                {
                    await _applicationUnitOfWork.ProductRepository.UpdateAsync(product);
                    await _applicationUnitOfWork.SaveAsync();

                    return ResultResponse.Success(200, "Product creaated successfully.");
                }
                else
                {
                    throw new DuplicateProductBarCodeException();
                }
            }
            catch (Exception ex)
            {
                return ResultResponse.Success(500, "Unable to create the product.");
            }
        }

        #endregion
    }
}
