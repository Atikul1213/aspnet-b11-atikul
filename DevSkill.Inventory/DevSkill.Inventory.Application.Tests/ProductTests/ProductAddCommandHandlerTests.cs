using Autofac.Extras.Moq;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.ProductTests;

[ExcludeFromCodeCoverage]
public class ProductAddCommandHandlerTests
{
    private AutoMock _moq;
    private Mock<IApplicationUnitOfWork> _applicationUnitOfWorkMock;
    private Mock<IProductRepository> _productRepositoryMock;

    [OneTimeSetUp] // This method is called once before any tests are run
    public void OneTimeSetup()
    {
        _moq = AutoMock.GetLoose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _moq.Dispose();
    }


    [SetUp]  // This method is called before each test is run
    public void Setup()
    {
        _applicationUnitOfWorkMock = _moq.Mock<IApplicationUnitOfWork>();
        _productRepositoryMock = _moq.Mock<IProductRepository>();
    }

    [TearDown] // This method is called after each test is run
    public void TearDown()
    {
        _applicationUnitOfWorkMock?.Reset();
        _productRepositoryMock?.Reset();
    }


    [Test]
    public void AddProduct_UniqueBarCode_AddsProduct()
    {
        var product = new Product
        {
            Name = "Test Product",
            BarCode = "1234567890123",
        };

        _applicationUnitOfWorkMock.SetupGet(p => p.ProductRepository).Returns(_productRepositoryMock.Object);

        //_productRepositoryMock.Setup(p => p.CheckBarCodeDuplicateAsync(product.BarCode, null).Result)
        //   .Returns(false).Verifiable;

        _productRepositoryMock.Setup(p => p.AddAsync(product)).Verifiable();
        _applicationUnitOfWorkMock.Setup(p => p.SaveAsync()).Verifiable();

        _applicationUnitOfWorkMock.Object.ProductRepository.AddAsync(product);
        _applicationUnitOfWorkMock.VerifyAll();
    }

    [Test]
    public void AddProduct_DuplicateBarCode_ThrowException()
    {

    }
}
