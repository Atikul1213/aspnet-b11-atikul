using Autofac.Extras.Moq;
using AutoMapper;
using DevSkill.Core.Application;
using DevSkill.Core.Application.UtilitiesContracts;
using DevSkill.Core.Domain.BusinessObjects;
using DevSkill.Inventory.Application.Features.ContactUs.SendMessage;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using Shouldly;

namespace DevSkill.Inventory.Application.Tests.ContactUs.SendMessage
{
    public class SendContactUsMessageCommandHandlerTest
    {
        #region Fields

        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private SendContactUsMessageCommandHandler _handler;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWorkMock;
        private Mock<IContactUsInfoRepository> _contactUsInfoRepositoryMock;
        private Mock<ICaptchaService> _captchaServiceMock;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            _mapperMock = _mock.Mock<IMapper>();
            _handler = _mock.Create<SendContactUsMessageCommandHandler>();
            _applicationUnitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _contactUsInfoRepositoryMock = _mock.Mock<IContactUsInfoRepository>();
            _captchaServiceMock = _mock.Mock<ICaptchaService>();
        }

        [TearDown]
        public void TearDown()
        {
            _mapperMock?.Reset();
            _applicationUnitOfWorkMock?.Reset();
            _contactUsInfoRepositoryMock?.Reset();
            _captchaServiceMock?.Reset();
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

        #region TestMethod

        [Test]
        public async Task SendContactUsMessageCommand_ReturnFail_WhenRequiredFieldsAreMissing()
        {
            // Assembley
            var command = new SendContactUsMessageCommand
            {
                Name = "",
                Email = "",
                Phone = null,
                Message = "",
                RecaptchaToken = "token"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.IsSuccess.ShouldBeFalse(),
                () => result.StatusCode.ShouldBe(400),
                () => result.Errors.ShouldContain("Name, Email and Message are required.")
            );
        }

        [Test]
        public async Task SendContactUsMessageCommand_ReturnFail_WhenRecaptchaMissing()
        {
            // Assembly 
            var command = new SendContactUsMessageCommand
            {
                Name = "Atikul",
                Email = "atikuldpi@gmail.com",
                Phone = "01722248512",
                Message = "Hello",
                RecaptchaToken = "invalid-token"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.IsSuccess.ShouldBeFalse(),
                () => result.StatusCode.ShouldBe(400),
                () => result.Errors.ShouldContain("ReCAPTCHA validation failed.")
            );
        }

        [Test]
        public async Task SendContactUsMessageCommand_ReturnSuccess_WhenValid()
        {
            // Assemble
            var command = new SendContactUsMessageCommand
            {
                Name = "Atikul",
                Email = "atikuldpi@gmail.com",
                Phone = "01722248512",
                Message = "Hello",
                RecaptchaToken = "valid-token"
            };

            _captchaServiceMock.Setup(c => c.VerifyAsync(command.RecaptchaToken))
                 .ReturnsAsync(new CaptchaResult(true, null));
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            this.ShouldSatisfyAllConditions(
                () => result.IsSuccess.ShouldBeTrue(),
                () => result.StatusCode.ShouldBe(200),
                () => ((ResultResponse<string>)result).Data.ShouldBe("Your message has been sent successfully.")
            );
        }


        #endregion
    }
}
