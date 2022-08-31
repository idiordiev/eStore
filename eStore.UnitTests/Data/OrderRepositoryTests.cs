using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Enums;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.Infrastructure.Data;
using eStore.Infrastructure.Data.Repositories;
using NUnit.Framework;

namespace eStore.UnitTests.Data
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task GetByIdAsync_ExistingOrder_ReturnsOrder(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Order> repo = new OrderRepository(context);
            var expected = UnitTestHelper.Orders.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await repo.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual order is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingOrder_ReturnsNull(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Order> repo = new OrderRepository(context);

            // Act
            var actual = await repo.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null order.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfOrders()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Order> repo = new OrderRepository(context);
            var expected = UnitTestHelper.Orders;
            
            // Act
            var actual = await repo.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of orders is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableOrders()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Order> repo = new OrderRepository(context);
            var expected = UnitTestHelper.Orders.Where(c => c.Id == 2);

            // Act
            var actual = repo.Query(c => c.Id == 2);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of orders is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewOrder_AddsOrderAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Order> repo = new OrderRepository(context);
            var newOrder = new Order()
            {
                IsDeleted = false, CustomerId = 1, Status = OrderStatus.New,
                TimeStamp = new DateTime(2022, 02, 11, 13, 45, 23),
                ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 98.97m,
                OrderItems = new List<OrderItem>()
                {
                    new()
                    {
                        IsDeleted = false, OrderId = 4, GoodsId = 15, Quantity = 1, UnitPrice = 48.99m
                    },
                    new()
                    {
                        IsDeleted = false, OrderId = 4, GoodsId = 2, Quantity = 2, UnitPrice = 24.99m
                    }
                }
            };
            
            // Act
            await repo.AddAsync(newOrder);

            // Assert
            Assert.AreEqual(7, context.Orders.Count(), "The new order has not been added to the context.");
            Assert.IsNotNull(await context.Orders.FindAsync(7), "The new order has been added with the wrong ID.");
            Assert.AreEqual(20, context.OrderItems.Count(), "The items of the new order has not been added to the context.");
        }

        [Test]
        public async Task UpdateAsync_ExistingOrder_UpdatesOrderAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Order> repo = new OrderRepository(context);
            var order = await context.Orders.FindAsync(1);
            
            // Act
            order.ShippingCity = "NewCity";
            await repo.UpdateAsync(order);

            // Assert
            Assert.AreEqual("NewCity", (await context.Orders.FindAsync(order.Id)).ShippingCity, "The order has not been updated.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DeleteAsync_ExistingOrder_DeletesOrderAndSavesToDb(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Order> repo = new OrderRepository(context);
            
            // Act
            await repo.DeleteAsync(id);

            // Assert
            Assert.AreEqual(5, context.Orders.Count(), "Any orders has not been deleted.");
            Assert.IsNull(await context.Orders.FindAsync(id), "The selected order has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingOrder_ThrowsArgumentNullException(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Order> repo = new OrderRepository(context);
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await repo.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}