using Autofac.Extras.Moq;
using AutoMapper;
using DevSkill.Inventory.Application.Features.ContactUs.GetContactUsInfo;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.ContactUs.GetContactUsInfo
{
    [ExcludeFromCodeCoverage]
    public class GetContactUsInfoQueryHandlerTests
    {
        #region Fields

        private AutoMock _moq;
        private Mock<IMapper> _mapperMock;
        private GetContactUsInfoQueryHandler _handler;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWorkMock;
        private Mock<IContactUsInfoRepository> _contactUsInfoRepositoryMock;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            _mapperMock = _moq.Mock<IMapper>();
            _handler = _moq.Create<GetContactUsInfoQueryHandler>();
            _applicationUnitOfWorkMock = _moq.Mock<IApplicationUnitOfWork>();
            _contactUsInfoRepositoryMock = _moq.Mock<IContactUsInfoRepository>();

            _applicationUnitOfWorkMock.SetupGet(u => u.ContactUsInfoRepository)
                .Returns(_contactUsInfoRepositoryMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _applicationUnitOfWorkMock?.Reset();
            _contactUsInfoRepositoryMock?.Reset();
            _mapperMock?.Reset();
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _moq = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _moq.Dispose();
        }

        #endregion

        #region Test Methods

        [Test]
        public async Task GetContactUsInfoQuery_ReturnContactUsInfo_WhenInfoExist()
        {
            // Arrange
            var contactUsInfo = new ContactUsInfo
            {
                Id = Guid.NewGuid(),
                Email = "atikuldpi@gmail.com",
                Phone = "01722248512",
                Address = "Khansama,Dinajpur"
            };

            _contactUsInfoRepositoryMock.Setup(c => c.GetContactUsInfoAsync())
                .ReturnsAsync(contactUsInfo);

            var query = new GetContactUsInfoQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Email.ShouldBe(contactUsInfo.Email),
                () => result.Phone.ShouldBe(contactUsInfo.Phone),
                () => result.Address.ShouldBe(contactUsInfo.Address)
             );
        }


        [Test]
        public async Task GetContactUsInfoQuery_ReturnNull_WhenNotExist()
        {
            // Arrage
            _contactUsInfoRepositoryMock.Setup(c => c.GetContactUsInfoAsync())
                .ReturnsAsync((ContactUsInfo?)null);

            var query = new GetContactUsInfoQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert

            this.ShouldSatisfyAllConditions(
                () => result.ShouldBeNull()
            );
        }


        [Test]
        public async Task GetContactUsInfoQuery_ReturnException_WhenInfoNotExist()
        {
            // Arrange
            _contactUsInfoRepositoryMock.Setup(c => c.GetContactUsInfoAsync())
                .ThrowsAsync(new Exception("DB failure"));

            var query = new GetContactUsInfoQuery();

            // Act
            var result = await Should.ThrowAsync<Exception>(async () =>
                await _handler.Handle(query, CancellationToken.None));

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Message.ShouldBe("DB failure")
             );
        }

        #endregion
    }
}
