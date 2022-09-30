﻿using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Exceptions;
using eStore.Application.Interfaces;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Application.Services;
using eStore.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Application
{
    [TestFixture]
    public class GoodsServiceTests
    {
        private UnitTestHelper _helper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        public async Task GetAllAsync_NotEmptyContext_ReturnsCollectionOfGoods()
        {
            // Arrange
            var expected = _helper.Goods;
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetAllAsync()).ReturnsAsync(expected);
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_ExistingGoods_ReturnsGoods()
        {
            // Arrange
            var expected = _helper.Goods.First();
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(1)).ReturnsAsync(expected);
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(1);

            // Assert
            Assert.AreEqual(expected, actual, "the actual goods is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_NotExistingGoods_ReturnsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Goods)null);
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);
            // Act
            var actual = await service.GetByIdAsync(1);

            // Assert
            Assert.IsNull(actual, "the method returned not-null object.");
        }

        [Test]
        public async Task GetGoodsInCustomerCartAsync_ExistingCustomerWithGoods_ReturnsCollectionOfGoods()
        {
            // Arrange
            var customer = _helper.Customers.First(c => c.Id == 1);
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(customer);
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var actual = (await service.GetGoodsInCustomerCartAsync(1)).ToList();

            // Assert
            Assert.AreEqual(6, actual.Count, "The collection contains the wrong number of goods.");
        }

        [Test]
        public void GetGoodsInCustomerCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.GetGoodsInCustomerCartAsync(1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void GetGoodsInCustomerCartAsync_DeactivatedCustomer_ThrowsAccountDeactivatedException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1))
                .ReturnsAsync(_helper.Customers.First(c => c.Id == 2));
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<AccountDeactivatedException>(
                    async () => await service.GetGoodsInCustomerCartAsync(1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw AccountDeactivatedException.");
        }

        [Test]
        public async Task CheckIfAddedToCartAsync_ExistingCustomerWithSpecifiedGoods_ReturnsTrue()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1))
                .ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var result = await service.CheckIfAddedToCartAsync(1, 1);

            // Assert
            Assert.IsTrue(result, "The method returned wrong result.");
        }


        [Test]
        public async Task CheckIfAddedToCartAsync_ExistingCustomerWithoutSpecifiedGoods_ReturnsFalse()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1))
                .ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var result = await service.CheckIfAddedToCartAsync(1, 5);

            // Assert
            Assert.IsFalse(result, "The method returned wrong result.");
        }

        [Test]
        public void CheckIfAddedToCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.CheckIfAddedToCartAsync(1, 1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void CheckIfAddedToCartAsync_DeactivatedCustomer_ThrowsAccountDeactivatedException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1))
                .ReturnsAsync(_helper.Customers.First(c => c.Id == 2));
            IGoodsService service = new GoodsService(_mockUnitOfWork.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<AccountDeactivatedException>(async () =>
                    await service.CheckIfAddedToCartAsync(1, 1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw AccountDeactivatedException.");
        }
    }
}