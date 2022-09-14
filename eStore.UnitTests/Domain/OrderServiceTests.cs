﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Enums;
using eStore.ApplicationCore.Exceptions;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.DomainServices;
using eStore.ApplicationCore.Interfaces.DTO;
using eStore.ApplicationCore.Services;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Domain
{
    [TestFixture]
    public class OrderServiceTests
    {
        internal class OrderAddress : IOrderAddress
        {
            public string ShippingCountry { get; set; }
            public string ShippingCity { get; set; }
            public string ShippingAddress { get; set; }
            public string ShippingPostalCode { get; set; }
        }
        
        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmailService = new Mock<IEmailService>();
            _mockAttachmentService = new Mock<IAttachmentService>();
        }

        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IEmailService> _mockEmailService;
        private Mock<IAttachmentService> _mockAttachmentService;

        [Test]
        public async Task GetOrdersByCustomerIdAsync_NotEmptyDb_ReturnsCollectionOfOrders()
        {
            // Arrange
            var expected = UnitTestHelper.Orders.Where(o => o.CustomerId == 1);
            _mockUnitOfWork.Setup(x => x.OrderRepository.Query(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns(expected);
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var actual = await service.GetOrdersByCustomerIdAsync(1);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetOrderByIdAsync_ExistingOrder_ReturnsOrder()
        {
            // Arrange
            var expected = UnitTestHelper.Orders.First(o => o.Id == 1);
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(1)).ReturnsAsync(expected);
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var actual = await service.GetOrderByIdAsync(1);

            // Assert
            Assert.AreEqual(expected, actual, "The actual order is not equal to expected.");
        }

        [Test]
        public async Task GetOrderByIdAsync_NotExistingOrder_ReturnsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(1)).ReturnsAsync((Order)null);
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var actual = await service.GetOrderByIdAsync(1);

            // Assert
            Assert.IsNull(actual, "The method returned not-null object.");
        }

        [Test]
        public async Task CreateOrderAsync_EverythingOk_CreatesOrderAndSendsEmail()
        {
            // Arrange
            var customer = UnitTestHelper.Customers.First(c => c.Id == 1);
            var goods = UnitTestHelper.Goods.First(g => g.Id == 1);
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(customer);
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(1)).ReturnsAsync(goods);
            _mockUnitOfWork.Setup(x => x.OrderRepository.AddAsync(It.IsAny<Order>()));
            _mockAttachmentService.Setup(x => x.CreateInvoice(It.IsAny<Order>()))
                .Returns("filepath.ext");
            _mockEmailService.Setup(x => x.SendPurchaseEmailAsyncAsync(It.IsAny<Order>(), It.IsAny<string>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            await service.CreateOrderAsync(1,
                new List<IOrderItem> { new OrderItem { GoodsId = 1, Quantity = 1 } }, new OrderAddress() 
                {
                    ShippingCountry = "Country1",
                    ShippingAddress = "Address1",
                    ShippingCity = "City1",
                    ShippingPostalCode = "02000"
                });

            // Assert
            _mockAttachmentService.Verify(x => x.CreateInvoice(It.IsAny<Order>()), Times.Once);
            _mockEmailService.Verify(x => x.SendPurchaseEmailAsyncAsync(It.IsAny<Order>(), "filepath.ext"), Times.Once);
            _mockUnitOfWork.Verify(
                x => x.OrderRepository.AddAsync(It.Is<Order>(o =>
                    o.Customer.Equals(customer) && o.Total == goods.Price && o.OrderItems.Count == 1 && 
                    o.ShippingCountry == "Country1" && o.ShippingAddress == "Address1" && o.ShippingCity == "City1" && 
                    o.ShippingPostalCode == "02000" 
                    && o.Status == OrderStatus.New && o.TimeStamp - DateTime.Now <= TimeSpan.FromMinutes(1))), Times.Once);
        }

        [Test]
        public void CreateOrderAsync_NotExistingCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            var goods = UnitTestHelper.Goods.First(g => g.Id == 1);
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync((Customer)null);
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(1)).ReturnsAsync(goods);
            _mockUnitOfWork.Setup(x => x.OrderRepository.AddAsync(It.IsAny<Order>()));
            _mockAttachmentService.Setup(x => x.CreateInvoice(It.IsAny<Order>()))
                .Returns("filepath.ext");
            _mockEmailService.Setup(x => x.SendPurchaseEmailAsyncAsync(It.IsAny<Order>(), It.IsAny<string>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<CustomerNotFoundException>(async () => await service.CreateOrderAsync(1,
                new List<IOrderItem> { new OrderItem { GoodsId = 1, Quantity = 1 } }, new OrderAddress
                {
                    ShippingCountry = "Country1",
                    ShippingAddress = "Address1",
                    ShippingCity = "City1",
                    ShippingPostalCode = "02000"
                }));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw CustomerNotFoundException.");
        }

        [Test]
        public void CreateOrderAsync_DeactivatedAccount_ThrowsAccountDeactivatedException()
        {
            // Arrange
            var customer = UnitTestHelper.Customers.First(c => c.Id == 2);
            var goods = UnitTestHelper.Goods.First(c => c.Id == 1);
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(customer);
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(1)).ReturnsAsync(goods);
            _mockUnitOfWork.Setup(x => x.OrderRepository.AddAsync(It.IsAny<Order>()));
            _mockAttachmentService.Setup(x => x.CreateInvoice(It.IsAny<Order>()))
                .Returns("filepath.ext");
            _mockEmailService.Setup(x => x.SendPurchaseEmailAsyncAsync(It.IsAny<Order>(), It.IsAny<string>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<AccountDeactivatedException>(async () => await service.CreateOrderAsync(
                1,
                new List<IOrderItem> { new OrderItem { GoodsId = 1, Quantity = 1 } }, new OrderAddress
                {
                    ShippingCountry = "Country1",
                    ShippingAddress = "Address1",
                    ShippingCity = "City1",
                    ShippingPostalCode = "02000"
                }));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw AccountDeactivatedException.");
        }

        [Test]
        public void CreateOrderAsync_NotExistingGoods_ThrowsGoodsNotFoundException()
        {
            // Arrange
            var customer = UnitTestHelper.Customers.First(c => c.Id == 1);
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(customer);
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(1)).ReturnsAsync((Goods)null);
            _mockUnitOfWork.Setup(x => x.OrderRepository.AddAsync(It.IsAny<Order>()));
            _mockAttachmentService.Setup(x => x.CreateInvoice(It.IsAny<Order>()))
                .Returns("filepath.ext");
            _mockEmailService.Setup(x => x.SendPurchaseEmailAsyncAsync(It.IsAny<Order>(), It.IsAny<string>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<GoodsNotFoundException>(async () => await service.CreateOrderAsync(1,
                new List<IOrderItem> { new OrderItem { GoodsId = 1, Quantity = 1 } }, new OrderAddress
                {
                    ShippingCountry = "Country1",
                    ShippingAddress = "Address1",
                    ShippingCity = "City1",
                    ShippingPostalCode = "02000"
                }));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw GoodsNotFoundException.");
        }

        [Test]
        public void CreateOrderAsync_GoodsDeleted_ThrowsEntityDeletedException()
        {
            // Arrange
            var customer = UnitTestHelper.Customers.First(c => c.Id == 1);
            var goods = UnitTestHelper.Goods.First(c => c.Id == 2);
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(customer);
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(1)).ReturnsAsync(goods);
            _mockUnitOfWork.Setup(x => x.OrderRepository.AddAsync(It.IsAny<Order>()));
            _mockAttachmentService.Setup(x => x.CreateInvoice(It.IsAny<Order>()))
                .Returns("filepath.ext");
            _mockEmailService.Setup(x => x.SendPurchaseEmailAsyncAsync(It.IsAny<Order>(), It.IsAny<string>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<EntityDeletedException>(async () => await service.CreateOrderAsync(1,
                new List<IOrderItem> { new OrderItem { GoodsId = 1, Quantity = 1 } }, new OrderAddress
                {
                    ShippingCountry = "Country1",
                    ShippingAddress = "Address1",
                    ShippingCity = "City1",
                    ShippingPostalCode = "02000"
                }));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw EntityDeletedException.");
        }

        [Test]
        public void CreateOrderAsync_GoodsQuantityLessThan1_ThrowsInvalidQuantityException()
        {
            // Arrange
            var customer = UnitTestHelper.Customers.First(c => c.Id == 1);
            var goods = UnitTestHelper.Goods.First(c => c.Id == 1);
            _mockUnitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(1)).ReturnsAsync(customer);
            _mockUnitOfWork.Setup(x => x.GoodsRepository.GetByIdAsync(1)).ReturnsAsync(goods);
            _mockUnitOfWork.Setup(x => x.OrderRepository.AddAsync(It.IsAny<Order>()));
            _mockAttachmentService.Setup(x => x.CreateInvoice(It.IsAny<Order>()))
                .Returns("filepath.ext");
            _mockEmailService.Setup(x => x.SendPurchaseEmailAsyncAsync(It.IsAny<Order>(), It.IsAny<string>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<InvalidQuantityException>(async () => await service.CreateOrderAsync(1,
                new List<IOrderItem> { new OrderItem { GoodsId = 1, Quantity = 0 } }, new OrderAddress
                {
                    ShippingCountry = "Country1",
                    ShippingAddress = "Address1",
                    ShippingCity = "City1",
                    ShippingPostalCode = "02000"
                }));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw InvalidQuantityException.");
        }

        [Test]
        public void PayOrderAsync_NotExistingOrder_ThrowsOrderNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Order)null);
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<OrderNotFoundException>(async () => await service.PayOrderAsync(1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw OrderNotFoundException.");
        }

        [Test]
        public async Task PayOrderAsync_StatusNew_UpdatesStatus()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(1))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 1));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            await service.PayOrderAsync(1);

            // Assert
            _mockUnitOfWork.Verify(
                x => x.OrderRepository.UpdateAsync(It.Is<Order>(o => o.Id == 1 && o.Status == OrderStatus.Paid)),
                Times.Once);
        }

        [Test]
        public void PayOrderAsync_StatusPaid_ThrowsStatusChangingException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(2))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 2));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<StatusUnchangeableException>(async () => await service.PayOrderAsync(2));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw StatusChangingException.");
        }

        [Test]
        public void PayOrderAsync_StatusProcessing_ThrowsStatusChangingException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(3))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 3));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<StatusUnchangeableException>(async () => await service.PayOrderAsync(3));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw StatusChangingException.");
        }

        [Test]
        public void PayOrderAsync_StatusSent_ThrowsStatusChangingException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(4))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 4));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<StatusUnchangeableException>(async () => await service.PayOrderAsync(4));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw StatusChangingException.");
        }

        [Test]
        public void PayOrderAsync_StatusReceived_ThrowsStatusChangingException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(5))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 5));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<StatusUnchangeableException>(async () => await service.PayOrderAsync(5));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw StatusChangingException.");
        }

        [Test]
        public void PayOrderAsync_StatusCancelled_ThrowsStatusChangingException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(6))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 6));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<StatusUnchangeableException>(async () => await service.PayOrderAsync(6));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw StatusChangingException.");
        }

        [Test]
        public void CancelOrderAsync_NotExistingOrder_ThrowsOrderNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Order)null);
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<OrderNotFoundException>(async () => await service.CancelOrderAsync(1));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw OrderNotFoundException.");
        }

        [Test]
        public async Task CancelOrderAsync_StatusNew_UpdatesStatus()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(1))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 1));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            await service.CancelOrderAsync(1);

            // Assert
            _mockUnitOfWork.Verify(
                x => x.OrderRepository.UpdateAsync(It.Is<Order>(o => o.Id == 1 && o.Status == OrderStatus.Cancelled)),
                Times.Once);
        }

        [Test]
        public async Task CancelOrderAsync_StatusPaid_UpdatesStatus()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(2))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 2));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            await service.CancelOrderAsync(2);

            // Assert
            _mockUnitOfWork.Verify(
                x => x.OrderRepository.UpdateAsync(It.Is<Order>(o => o.Id == 2 && o.Status == OrderStatus.Cancelled)),
                Times.Once);
        }

        [Test]
        public async Task CancelOrderAsync_StatusProcessing_UpdatesStatus()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(3))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 3));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            await service.CancelOrderAsync(3);

            // Assert
            _mockUnitOfWork.Verify(
                x => x.OrderRepository.UpdateAsync(It.Is<Order>(o => o.Id == 3 && o.Status == OrderStatus.Cancelled)),
                Times.Once);
        }

        [Test]
        public async Task CancelOrderAsync_StatusSent_UpdatesStatus()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(4))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 4));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            await service.CancelOrderAsync(4);

            // Assert
            _mockUnitOfWork.Verify(
                x => x.OrderRepository.UpdateAsync(It.Is<Order>(o => o.Id == 4 && o.Status == OrderStatus.Cancelled)),
                Times.Once);
        }

        [Test]
        public void CancelOrderAsync_StatusReceived_ThrowsStatusChangingException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(5))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 5));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<StatusUnchangeableException>(async () => await service.CancelOrderAsync(5));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw StatusChangingException.");
        }

        [Test]
        public void CancelOrderAsync_StatusCancelled_ThrowsStatusChangingException()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetByIdAsync(6))
                .ReturnsAsync(UnitTestHelper.Orders.First(o => o.Id == 6));
            _mockUnitOfWork.Setup(x => x.OrderRepository.UpdateAsync(It.IsAny<Order>()));
            IOrderService service = new OrderService(_mockUnitOfWork.Object, _mockEmailService.Object,
                _mockAttachmentService.Object);

            // Act
            var exception = Assert.ThrowsAsync<StatusUnchangeableException>(async () => await service.CancelOrderAsync(6));

            // Assert
            Assert.IsNotNull(exception, "The method didn't throw StatusChangingException.");
        }
    }
}