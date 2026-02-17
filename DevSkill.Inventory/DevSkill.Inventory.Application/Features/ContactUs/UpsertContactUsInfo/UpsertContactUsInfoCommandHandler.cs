using AutoMapper;
using Cortex.Mediator.Commands;
using DevSkill.Core.Application;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.ContactUs.UpsertContactUsInfo
{
    public class UpsertContactUsInfoCommandHandler : ICommandHandler<UpsertContactUsInfoCommand, ResultResponse<ContactUsInfo>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UpsertContactUsInfoCommandHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<ResultResponse<ContactUsInfo>> Handle(UpsertContactUsInfoCommand command, CancellationToken cancellationToken)
        {
            var contactUsInfo = await _applicationUnitOfWork.ContactUsInfoRepository.GetContactUsInfoAsync();

            if (contactUsInfo == null)
            {
                contactUsInfo = _mapper.Map<ContactUsInfo>(command);

                if (contactUsInfo.Id == Guid.Empty)
                    contactUsInfo.Id = Guid.NewGuid();

                await _applicationUnitOfWork.ContactUsInfoRepository.AddAsync(contactUsInfo);
                await _applicationUnitOfWork.SaveAsync();
            }
            else
            {
                command.Id = contactUsInfo.Id;
                contactUsInfo = _mapper.Map<ContactUsInfo>(command);
                await _applicationUnitOfWork.ContactUsInfoRepository.UpdateAsync(contactUsInfo);
                await _applicationUnitOfWork.SaveAsync();
            }

            return ResultResponse.Success(200, contactUsInfo);
        }

        #endregion
    }
}
