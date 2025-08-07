using Autofac.Extras.Moq;
using AutoMapper;
using DevSkill.Inventory.Application.Features.SalesProduct.Commands;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.SalesTests
{
    [ExcludeFromCodeCoverage]
    public class SalesUpdateCommandHandlerTests
    {
        private AutoMock _moq;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private Mock<ISalesRepository> _salesRepoMock;
        private Mock<IMapper> _mapperMock;

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

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = _moq.Mock<IApplicationUnitOfWork>();
            _salesRepoMock = _moq.Mock<ISalesRepository>();
            _mapperMock = _moq.Mock<IMapper>();

            _unitOfWorkMock.SetupGet(x => x.SalesRepository).Returns(_salesRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWorkMock?.Reset();
            _salesRepoMock?.Reset();
            _mapperMock?.Reset();
        }

        [Test]
        public async Task UpdateSales_ValidCommand_ReturnsUpdatedSales()
        {
            // Arrange
            var command = new SalesUpdateCommand
            {
                Id = Guid.NewGuid(),
                InvoiceNo = "INV-1002",
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Jane Doe",
                CustomerPhoneNumber = "0987654321",
                StatusId = 2,
                SalesTypeId = 1,
                Vat = 6.00m,
                NetAmount = 200.00m,
                Discount = 20.00m,
                TotalAmount = 186.00m,
                PaidAmount = 180.00m,
                DueAmount = 6.00m,
                AccountTypeId = 1,
                AccountNoId = Guid.NewGuid(),
                Note = "Updated Note",
                TermsAndConditions = "Updated T&C"
            };

            var mappedSales = new Sales
            {
                Id = command.Id,
                InvoiceNo = command.InvoiceNo,
                SaleDate = command.SaleDate,
                CustomerId = command.CustomerId,
                CustomerName = command.CustomerName,
                CustomerPhoneNumber = command.CustomerPhoneNumber,
                StatusId = command.StatusId,
                SalesTypeId = command.SalesTypeId,
                Vat = command.Vat,
                NetAmount = command.NetAmount,
                Discount = command.Discount,
                TotalAmount = command.TotalAmount,
                PaidAmount = command.PaidAmount,
                DueAmount = command.DueAmount,
                AccountTypeId = command.AccountTypeId,
                AccountNoId = command.AccountNoId,
                Note = command.Note,
                TermsAndConditions = command.TermsAndConditions
            };

            var updatedSales = new Sales
            {
                Id = mappedSales.Id,
                InvoiceNo = mappedSales.InvoiceNo,
            };

            _mapperMock.Setup(m => m.Map<Sales>(command)).Returns(mappedSales);

            _salesRepoMock.Setup(r => r.UpdateSalesAsync(mappedSales))
                .ReturnsAsync(updatedSales)
                .Verifiable();

            _unitOfWorkMock.Setup(u => u.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new SalesUpdateCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _mapperMock.Verify(m => m.Map<Sales>(command), Times.Once);
            _salesRepoMock.Verify(r => r.UpdateSalesAsync(mappedSales), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(updatedSales.Id));
            Assert.That(result.InvoiceNo, Is.EqualTo(command.InvoiceNo));
        }
    }
}
