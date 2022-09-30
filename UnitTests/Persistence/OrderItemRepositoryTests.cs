using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Persistence.Repositories;
using NUnit.Framework;

namespace eStore.UnitTests.Persistence
{
    [TestFixture]
    public class OrderItemRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<OrderItem> _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new OrderItemRepository(_context);
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
        public async Task GetByIdAsync_ExistingOrderItem_ReturnsOrderItem(int id)
        {
            // Arrange
            var expected = _helper.OrderItems.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await _repository.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual order item is not equal to the expected.");
        }

        [TestCase(19)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingOrderItem_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null order item.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfOrderItems()
        {
            // Arrange
            var expected = _helper.OrderItems;
            
            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of order items is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableOrderItems()
        {
            // Arrange
            var expected = _helper.OrderItems.Where(c => c.Id == 13);

            // Act
            var actual = _repository.Query(c => c.Id == 13);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of order items is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewOrderItem_AddsOrderItemAndSavesToDb()
        {
            // Arrange
            var newOrderItem = new OrderItem()
            {
                IsDeleted = false, OrderId = 4, GoodsId = 2, Quantity = 1, UnitPrice = 24.99m
            };
            
            // Act
            await _repository.AddAsync(newOrderItem);

            // Assert
            Assert.AreEqual(19, _context.OrderItems.Count(), "The new order item has not been added to the context.");
            Assert.IsNotNull(await _context.OrderItems.FindAsync(19), "The new order item has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingOrderItem_UpdatesOrderItemAndSavesToDb()
        {
            // Arrange
            var orderItem = await _context.OrderItems.FindAsync(12);
            
            // Act
            orderItem.OrderId = 4;
            await _repository.UpdateAsync(orderItem);

            // Assert
            Assert.AreEqual(4, (await _context.OrderItems.FindAsync(orderItem.Id)).OrderId, "The order item has not been updated.");
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
            
            // Act
            await _repository.DeleteAsync(id);

            // Assert
            Assert.AreEqual(17, _context.OrderItems.Count(), "Any order items has not been deleted.");
            Assert.IsNull(await _context.OrderItems.FindAsync(id), "The selected order item has not been deleted.");
        }

        [TestCase(19)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingOrderItem_ThrowsArgumentNullException(int id)
        {
            // Arrange
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}