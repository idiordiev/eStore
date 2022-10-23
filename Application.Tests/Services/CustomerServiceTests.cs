using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.Exceptions;
using eStore.Application.Interfaces;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Application.Services;
using eStore.Domain.Entities;
using eStore.Tests.Common;
using eStore.Tests.Common.EqualityComparers;
using Moq;
using NUnit.Framework;

namespace eStore.Application.Tests.Services
{
    [TestFixture]
    public class CustomerServiceTests
    {
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _mockEmailService = new Mock<IEmailService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        private UnitTestHelper _helper;
        private Mock<IEmailService> _mockEmailService;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        [Test]
        public async Task GetCustomerByIdAsync_ExistingCustomer_ReturnsCustomer()
        {
            // Arrange
            var expected = _helper.Customers.First(c => c.Id == 1);
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(expected);
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var actual = await service.GetCustomerByIdAsync(1);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new CustomerEqualityComparer()),
                "The actual customer is not equal to expected.");
        }

        [Test]
        public async Task GetCustomerByIdAsync_NotExistingCustomer_ReturnsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var actual = await service.GetCustomerByIdAsync(1);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null object.");
        }

        [Test]
        public async Task AddCustomerAsync_CustomerWithNewEmail_AddCustomerAndSendsEmail()
        {
            // Arrange
            _mockEmailService.Setup(x => x.SendRegisterEmailAsync(It.IsAny<Customer>()));
            _mockUnitOfWork.Setup(x => x.CustomerRepository.Query(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns(new List<Customer>());
            _mockUnitOfWork.Setup(x => x.CustomerRepository.AddAsync(It.IsAny<Customer>()));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);
            var customer = new Customer
            {
                IsDeleted = false, Email = "email3@mail.com", IdentityId = Guid.NewGuid().ToString(),
                ShoppingCart = new ShoppingCart()
            };

            // Act
            await service.AddCustomerAsync(customer);

            // Assert
            _mockUnitOfWork.Verify(
                x => x.CustomerRepository.AddAsync(It.Is<Customer>(c => c.Email == "email3@mail.com" && c.ShoppingCart != null)),
                Times.Once);
            _mockEmailService.Verify(
                x => x.SendRegisterEmailAsync(It.Is<Customer>(c => c.Email == "email3@mail.com" && c.ShoppingCart != null)), Times.Once);
        }

        [Test]
        public void AddCustomerAsync_CustomerWithExistingEmail_ThrowsEmailNotUniqueException()
        {
            // Arrange
            _mockEmailService.Setup(x => x.SendRegisterEmailAsync(It.IsAny<Customer>()));
            _mockUnitOfWork.Setup(x => x.CustomerRepository.Query(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns(new List<Customer>(_helper.Customers.Where(c => c.Email == "email1@mail.com")));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);
            var customer = new Customer
            {
                IsDeleted = false, Email = "email1@mail.com", IdentityId = Guid.NewGuid().ToString(),
                ShoppingCart = new ShoppingCart()
            };

            // Act
            var exception = Assert.ThrowsAsync<EmailNotUniqueException>(async () => await service.AddCustomerAsync(customer));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw EmailNotUniqueException.");
        }

        [Test]
        public async Task UpdateCustomerInfoAsync_ExistingCustomer_UpdatesCustomer()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.UpdateAsync(It.IsAny<Customer>()));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);
            var customer = _helper.Customers.First();
            customer.FirstName = "NewName";

            // Act
            await service.UpdateCustomerInfoAsync(customer);

            // Assert
            _mockUnitOfWork.Verify(x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c => c == customer)));
        }

        [Test]
        public async Task DeactivateAccountAsync_ExistingAccount_DeactivatesAccountAndSendsEmail()
        {
            // Arrange
            _mockEmailService.Setup(x => x.SendDeactivationEmailAsync(It.IsAny<string>()));
            _mockUnitOfWork.Setup(x => x.CustomerRepository.UpdateAsync(It.IsAny<Customer>()));
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            await service.DeactivateAccountAsync(1);

            // Assert
            _mockUnitOfWork.Verify(
                x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c => c.Id == 1 && c.IsDeleted == true && c.Email == null)),
                Times.Once);
            _mockEmailService.Verify(x => x.SendDeactivationEmailAsync("email1@mail.com"), Times.Once);
        }

        [Test]
        public void DeactivateAccount_NotExistingAccount_ThrowsCustomerNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.DeactivateAccountAsync(1));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void DeactivateAccount_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(2)).ReturnsAsync(_helper.Customers.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<AccountDeactivatedException>(async () => await service.DeactivateAccountAsync(2));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw AccountDeactivatedException.");
        }

        [Test]
        public async Task AddGoodsToCartAsync_ExistingCustomerAndGoods_AddsGoodsToCustomerCart()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(5)).ReturnsAsync(_helper.Goods.First(c => c.Id == 5));
            _mockUnitOfWork.Setup(x => x.CustomerRepository.UpdateAsync(It.IsAny<Customer>()));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            await service.AddGoodsToCartAsync(1, 5);

            // Assert
            _mockUnitOfWork.Verify(x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c => c.ShoppingCart.Goods.Any(g => g.Id == 1))));
        }

        [Test]
        public void AddGoodsToCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.AddGoodsToCartAsync(1, 1));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void AddGoodsToCartAsync_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_helper.Customers.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<AccountDeactivatedException>(async () => await service.AddGoodsToCartAsync(1, 1));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw AccountDeactivatedException.");
        }

        [Test]
        public void AddGoodsToCartAsync_GoodsAlreadyAddedToCart_ThrowsGoodsAlreadyAddedException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<GoodsAlreadyAddedException>(async () => await service.AddGoodsToCartAsync(1, 1));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw GoodsAlreadyAddedException.");
        }

        [Test]
        public void AddGoodsToCartAsync_NotExistingGoods_ThrowsGoodsNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Goods)null);
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<GoodsNotFoundException>(async () => await service.AddGoodsToCartAsync(1, 17));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw GoodsNotFoundException.");
        }

        [Test]
        public void AddGoodsToCartAsync_DeletedGoods_ThrowsEntityDeletedException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_helper.Goods.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<EntityDeletedException>(async () => await service.AddGoodsToCartAsync(1, 13));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw EntityDeletedException.");
        }

        [Test]
        public async Task RemoveGoodsFromCartAsync_ExistingCustomerAndGoods_RemovesGoodsFromCustomerCart()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            await service.RemoveGoodsFromCartAsync(1, 1);

            // Assert
            _mockUnitOfWork.Verify(x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c => c.ShoppingCart.Goods.All(g => g.Id != 1))),
                Times.Once);
        }

        [Test]
        public void RemoveGoodsFromCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.RemoveGoodsFromCartAsync(1, 1));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void RemoveGoodsFromCartAsync_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(2)).ReturnsAsync(_helper.Customers.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<AccountDeactivatedException>(async () => await service.RemoveGoodsFromCartAsync(2, 1));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw AccountDeactivatedException.");
        }

        [Test]
        public void RemoveGoodsFromCartAsync_NotExistingGoods_ThrowsGoodsNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<GoodsNotFoundException>(async () => await service.RemoveGoodsFromCartAsync(1, 5));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw GoodsNotFoundException.");
        }

        [Test]
        public async Task ClearCustomerCartAsync_ExistingCustomer_RemovesGoodsFromCustomerCart()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(_helper.Customers.First(c => c.Id == 1));
            _mockUnitOfWork.Setup(x => x.CustomerRepository.UpdateAsync(It.IsAny<Customer>()));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            await service.ClearCustomerCartAsync(1);

            // Assert
            _mockUnitOfWork.Verify(x => x.CustomerRepository.UpdateAsync(It.Is<Customer>(c => c.Id == 1 && !c.ShoppingCart.Goods.Any())),
                Times.Once);
        }

        [Test]
        public void ClearCustomerCartAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.ClearCustomerCartAsync(1));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void ClearCustomerCartAsync_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(2)).ReturnsAsync(_helper.Customers.First(c => c.Id == 2));
            ICustomerService service = new CustomerService(_mockUnitOfWork.Object, _mockEmailService.Object);

            // Act
            var exception = Assert.ThrowsAsync<AccountDeactivatedException>(async () => await service.ClearCustomerCartAsync(2));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method didn't throw AccountDeactivatedException.");
        }
    }
}