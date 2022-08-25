using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.Infrastructure.Data;
using eStore.Infrastructure.Data.Repositories;
using NUnit.Framework;

namespace eStore.UnitTests.Data
{
    [TestFixture]
    public class OrderItemRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        public async Task GetByIdAsync_ExistingOrderItem_ReturnsOrderItem(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<OrderItem> repo = new OrderItemRepository(context);
            var expected = UnitTestHelper.OrderItems.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await repo.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual order item is not equal to the expected.");
        }

        [TestCase(16)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingOrderItem_ReturnsNull(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<OrderItem> repo = new OrderItemRepository(context);

            // Act
            var actual = await repo.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null order item.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfOrderItems()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<OrderItem> repo = new OrderItemRepository(context);
            var expected = UnitTestHelper.OrderItems;
            
            // Act
            var actual = await repo.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of order items is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableOrderItems()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<OrderItem> repo = new OrderItemRepository(context);
            var expected = UnitTestHelper.OrderItems.Where(c => c.Id == 13);

            // Act
            var actual = repo.Query(c => c.Id == 13);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of order items is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewOrderItem_AddsOrderItemAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<OrderItem> repo = new OrderItemRepository(context);
            var newOrderItem = new OrderItem()
            {
                IsDeleted = false, OrderId = 4, GoodsId = 2, Quantity = 1, UnitPrice = 24.99m
            };
            
            // Act
            await repo.AddAsync(newOrderItem);

            // Assert
            Assert.AreEqual(15, context.OrderItems.Count(), "The new order item has not been added to the context.");
            Assert.IsNotNull(await context.OrderItems.FindAsync(15), "The new order item has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingOrderItem_UpdatesOrderItemAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<OrderItem> repo = new OrderItemRepository(context);
            var orderItem = await context.OrderItems.FindAsync(12);
            
            // Act
            orderItem.OrderId = 4;
            await repo.UpdateAsync(orderItem);

            // Assert
            Assert.AreEqual(4, (await context.OrderItems.FindAsync(orderItem.Id)).OrderId, "The order item has not been updated.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        public async Task DeleteAsync_ExistingOrderItem_DeletesOrderItemAndSavesToDb(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<OrderItem> repo = new OrderItemRepository(context);
            
            // Act
            await repo.DeleteAsync(id);

            // Assert
            Assert.AreEqual(13, context.OrderItems.Count(), "Any order items has not been deleted.");
            Assert.IsNull(await context.OrderItems.FindAsync(id), "The selected order item has not been deleted.");
        }

        [TestCase(16)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingOrderItem_ThrowsArgumentNullException(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<OrderItem> repo = new OrderItemRepository(context);
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await repo.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}