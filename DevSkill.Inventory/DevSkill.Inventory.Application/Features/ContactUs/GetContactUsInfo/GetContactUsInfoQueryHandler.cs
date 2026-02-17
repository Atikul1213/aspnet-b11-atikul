using AutoMapper;
using Cortex.Mediator.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.ContactUs.GetContactUsInfo
{
    public class GetContactUsInfoQueryHandler : IQueryHandler<GetContactUsInfoQuery, ContactUsInfo>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public GetContactUsInfoQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<ContactUsInfo> Handle(GetContactUsInfoQuery query, CancellationToken cancellationToken)
        {
            var contactUsInfo = await _applicationUnitOfWork.ContactUsInfoRepository.GetContactUsInfoAsync();

            return contactUsInfo;
        }
        #endregion
    }
}
