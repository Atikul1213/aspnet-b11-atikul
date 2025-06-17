using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Categories.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UpdateCategoryCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            var category = _mapper.Map<Category>(request);
            category.CreateOnUtc = DateTime.UtcNow;

            await _applicationUnitOfWork.CategoryRepository.UpdateAsync(category);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
