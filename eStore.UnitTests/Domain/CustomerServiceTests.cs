using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class CustomerServiceTests
    {
        [Test]
        public async Task GetCustomerByIdAsync_ExistingCustomer_ReturnsCustomer()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var expected = UnitTestHelper.Customers.First(c => c.Id == 1);
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1))
                .ReturnsAsync(expected);
            var mockEmailService = new Mock<IEmailService>();
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var actual = await service.GetCustomerByIdAsync(1);

            // Assert
            Assert.AreEqual(expected, actual, "The actual customer is not equal to expected.");
        }

        [Test]
        public async Task GetCustomerByIdAsync_NotExistingCustomer_ReturnsNull()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync((Customer)null);
            var mockEmailService = new Mock<IEmailService>();
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var actual = await service.GetCustomerByIdAsync(1);

            // Assert
            Assert.IsNull(actual, "The method returned not-null object.");
        }

        [Test]
        public async Task AddCustomerAsync_CustomerWithNewEmail_AddCustomerAndSendsEmail()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.SendRegisterEmailAsync(It.IsAny<Customer>()));
            mockUnitOfWork.Setup(x => x.CustomerRepository.Query(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns(new List<Customer>());
            mockUnitOfWork.Setup(x => x.CustomerRepository.AddAsync(It.IsAny<Customer>()));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);
            var customer = new Customer
            {
                IsDeleted = false, Email = "email3@mail.com", IdentityId = Guid.NewGuid().ToString(),
                ShoppingCart = new ShoppingCart()
            };

            // Act
            await service.AddCustomerAsync(customer);

            // Assert
            mockUnitOfWork.Verify(
                x => x.CustomerRepository.AddAsync(It.Is<Customer>(c =>
                    c.Email == "email3@mail.com" && c.ShoppingCart != null)), Times.Once);
            mockEmailService.Verify(
                x => x.SendRegisterEmailAsync(It.Is<Customer>(c =>
                    c.Email == "email3@mail.com" && c.ShoppingCart != null)), Times.Once);
        }

        [Test]
        public void AddCustomerAsync_CustomerWithExistingEmail_ThrowsEmailNotUniqueException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.SendRegisterEmailAsync(It.IsAny<Customer>()));
            mockUnitOfWork.Setup(x => x.CustomerRepository.Query(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns(new List<Customer>(UnitTestHelper.Customers.Where(c => c.Email == "email1@mail.com")));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);
            var customer = new Customer
            {
                IsDeleted = false, Email = "email1@mail.com", IdentityId = Guid.NewGuid().ToString(),
                ShoppingCart = new ShoppingCart()
            };

            // Act
            var exception =
                Assert.ThrowsAsync<EmailNotUniqueException>(async () => await service.AddCustomerAsync(customer));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw EmailNotUniqueException.");
        }

        [Test]
        public async Task UpdateCustomerInfoAsync_ExistingCustomer_UpdatesCustomer()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.UpdateAsync(It.IsAny<Customer>()));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);
            var customer = UnitTestHelper.Customers.First();
            customer.FirstName = "NewName";

            // Act
            await service.UpdateCustomerInfoAsync(customer);

            // Assert
            mockUnitOfWork.Verify(x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c => c == customer)));
        }

        [Test]
        public async Task DeactivateAccountAsync_ExistingAccount_DeactivatesAccountAndSendsEmail()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.SendDeactivationEmailAsync(It.IsAny<string>()));
            mockUnitOfWork.Setup(x => x.CustomerRepository.UpdateAsync(It.IsAny<Customer>()));
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1))
                .ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            await service.DeactivateAccountAsync(1);

            // Assert
            mockUnitOfWork.Verify(
                x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c =>
                    c.Id == 1 && c.IsDeleted == true && c.Email == null)), Times.Once);
            mockEmailService.Verify(x => x.SendDeactivationEmailAsync("email1@mail.com"), Times.Once);
        }

        [Test]
        public void DeactivateAccount_NotExistingAccount_ThrowsCustomerNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.DeactivateAccountAsync(1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void DeactivateAccount_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(2)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<AccountDeactivatedException>(async () => await service.DeactivateAccountAsync(2));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw AccountDeactivatedException.");
        }

        [Test]
        public async Task AddGoodsToCartAsync_ExistingCustomerAndGoods_AddsGoodsToCustomerCart()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1))
                .ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(5))
                .ReturnsAsync(UnitTestHelper.Goods.First(c => c.Id == 5));
            mockUnitOfWork.Setup(x => x.CustomerRepository.UpdateAsync(It.IsAny<Customer>()));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            await service.AddGoodsToCartAsync(1, 5);

            // Assert
            mockUnitOfWork.Verify(x =>
                x.CustomerRepository.UpdateAsync(It.Is<Customer>(c =>
                    c.ShoppingCart.Goods.Any(g => g.CartId == 1 && g.GoodsId == 1))));
        }

        [Test]
        public void AddGoodsToCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.AddGoodsToCartAsync(1, 1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void AddGoodsToCartAsync_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<AccountDeactivatedException>(async () => await service.AddGoodsToCartAsync(1, 1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw AccountDeactivatedException.");
        }

        [Test]
        public void AddGoodsToCartAsync_GoodsAlreadyAddedToCart_ThrowsGoodsAlreadyAddedException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<GoodsAlreadyAddedException>(async () => await service.AddGoodsToCartAsync(1, 1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw GoodsAlreadyAddedException.");
        }

        [Test]
        public void AddGoodsToCartAsync_NotExistingGoods_ThrowsGoodsNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Goods)null);
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<GoodsNotFoundException>(async () => await service.AddGoodsToCartAsync(1, 17));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw GoodsNotFoundException.");
        }

        [Test]
        public void AddGoodsToCartAsync_DeletedGoods_ThrowsEntityDeletedException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(UnitTestHelper.Goods.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<EntityDeletedException>(async () => await service.AddGoodsToCartAsync(1, 13));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw EntityDeletedException.");
        }

        [Test]
        public async Task RemoveGoodsFromCartAsync_ExistingCustomerAndGoods_RemovesGoodsFromCustomerCart()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            await service.RemoveGoodsFromCartAsync(1, 1);

            // Assert
            mockUnitOfWork.Verify(
                x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c =>
                    !c.ShoppingCart.Goods.Any(g => g.CartId == 1 && g.GoodsId == 1))), Times.Once);
        }

        [Test]
        public void RemoveGoodsFromCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.RemoveGoodsFromCartAsync(1, 1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void RemoveGoodsFromCartAsync_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(2)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<AccountDeactivatedException>(
                    async () => await service.RemoveGoodsFromCartAsync(2, 1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw AccountDeactivatedException.");
        }

        [Test]
        public void RemoveGoodsFromCartAsync_NotExistingGoods_ThrowsGoodsNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<GoodsNotFoundException>(async () => await service.RemoveGoodsFromCartAsync(1, 5));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw GoodsNotFoundException.");
        }
        
        [Test]
        public async Task ClearCustomerCartAsync_ExistingCustomer_RemovesGoodsFromCustomerCart()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 1));
            mockUnitOfWork.Setup(x => x.CustomerRepository.UpdateAsync(It.IsAny<Customer>()));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            await service.ClearCustomerCartAsync(1);

            // Assert
            mockUnitOfWork.Verify(x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c => c.Id == 1 && !c.ShoppingCart.Goods.Any())), Times.Once);
        }

        [Test]
        public void ClearCustomerCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.ClearCustomerCartAsync(1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void ClearCustomerCartAsync_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(2)).ReturnsAsync(UnitTestHelper.Customers.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(mockUnitOfWork.Object, mockEmailService.Object);

            // Act
            var exception =
                Assert.ThrowsAsync<AccountDeactivatedException>(async () => await service.ClearCustomerCartAsync(2));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw AccountDeactivatedException.");
        }
    }
}