using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Exceptions;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Services;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Domain
{
    [TestFixture]
    public class GoodsServiceTests
    {
        [Test]
        public async Task GetAllAsync_NotEmptyContext_ReturnsCollectionOfGoods()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var expected = UnitTestHelper.Goods;
            mockUnitOfWork.Setup(x => x.GoodsRepository.GetAllAsync()).ReturnsAsync(expected);
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

            // Act
            var actual = await service.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_ExistingGoods_ReturnsGoods()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var expected = UnitTestHelper.Goods.First();
            mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(1)).ReturnsAsync(expected);
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(1);

            // Assert
            Assert.AreEqual(expected, actual, "the actual goods is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_NotExistingGoods_ReturnsNull()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Goods)null);
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);
            // Act
            var actual = await service.GetByIdAsync(1);

            // Assert
            Assert.IsNull(actual, "the method returned not-null object.");
        }

        [Test]
        public async Task GetGoodsInCustomerCartAsync_ExistingCustomerWithGoods_ReturnsCollectionOfGoods()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var customer = UnitTestHelper.Customers.First(c => c.Id == 1);
            foreach (var goodsInCart in customer.ShoppingCart.Goods)
            {
                goodsInCart.Goods = UnitTestHelper.Goods.FirstOrDefault(g => g.Id == goodsInCart.GoodsId);
            }
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(customer);
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

            // Act
            var actual = (await service.GetGoodsInCustomerCartAsync(1)).ToList();

            // Assert
            Assert.AreEqual(6, actual.Count, "The collection contains the wrong number of goods.");
        }

        [Test]
        public void GetGoodsInCustomerCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

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
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 2));
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

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
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

            // Act
            var result = await service.CheckIfAddedToCartAsync(1, 1);

            // Assert
            Assert.IsTrue(result, "The method returned wrong result.");
        }


        [Test]
        public async Task CheckIfAddedToCartAsync_ExistingCustomerWithoutSpecifiedGoods_ReturnsFalse()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

            // Act
            var result = await service.CheckIfAddedToCartAsync(1, 5);

            // Assert
            Assert.IsFalse(result, "The method returned wrong result.");
        }

        [Test]
        public void CheckIfAddedToCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

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
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 2));
            IGoodsService service = new GoodsService(mockUnitOfWork.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<AccountDeactivatedException>(async () =>
                    await service.CheckIfAddedToCartAsync(1, 1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw AccountDeactivatedException.");
        }
    }
}