using Autofac.Extras.Moq;
using AutoMapper;
using DevSkill.Inventory.Application.Features.ContactUs.SendMessage;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Repositories;
using Moq;

namespace DevSkill.Inventory.Application.Tests.ContactUs.UpsertContactUsInfo
{
    public class UpsertContactUsInfoCommandHandler
    {
        #region Fields

        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private SendContactUsMessageCommandHandler _handler;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWorkMock;
        private Mock<IContactUsInfoRepository> _contactUsInfoRepositoryMock;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            _mapperMock = _mock.Mock<IMapper>();
            _handler = _mock.Create<SendContactUsMessageCommandHandler>();
            _applicationUnitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _contactUsInfoRepositoryMock = _mock.Mock<IContactUsInfoRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _mapperMock?.Reset();
            _applicationUnitOfWorkMock?.Reset();
            _contactUsInfoRepositoryMock?.Reset();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        #endregion

        #region Test Methods


        #endregion
    }
}
