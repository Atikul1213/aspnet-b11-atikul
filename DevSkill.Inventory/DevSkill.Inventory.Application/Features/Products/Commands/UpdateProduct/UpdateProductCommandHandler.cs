using AutoMapper;
using Cortex.Mediator.Commands;
using DevSkill.Core.Application;
using DevSkill.Inventory.Domain;

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
                var product = await _applicationUnitOfWork.ProductRepository.GetByIdAsync(command.Id);
                if (product == null)
                {
                    return ResultResponse.Success(404, "Product not found.");
                }

                _mapper.Map(command, product);

                if (product.BarCode == command.BarCode)
                {
                    await _applicationUnitOfWork.SaveAsync();

                    return ResultResponse.Success(200, "Product creaated successfully.");
                }
                else
                {
                    return ResultResponse.Success(300, "Unable to create the product. Product Id or barcode does not match");
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
