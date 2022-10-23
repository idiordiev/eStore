using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using eStore.Domain.Enums;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Persistence.Repositories;
using eStore.Tests.Common;
using NUnit.Framework;

namespace eStore.Infrastructure.Tests.Persistence
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new OrderRepository(_context);
        }

        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Order> _repository;

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task GetByIdAsync_ExistingOrder_ReturnsOrder(int id)
        {
            // Arrange
            var expected = _helper.Orders.FirstOrDefault(c => c.Id == id);

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual order is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingOrder_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null order.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfOrders()
        {
            // Arrange
            var expected = _helper.Orders;

            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual collection of orders is not equal to the expected.");
        }

        [Test]
        public Task Query_WithPredicate_ReturnsSuitableOrders()
        {
            // Arrange
            var expected = _helper.Orders.Where(c => c.Id == 2);

            // Act
            var actual = _repository.Query(c => c.Id == 2);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual collection of orders is not equal to the expected.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddAsync_NewOrder_AddsOrderAndSavesToDb()
        {
            // Arrange
            var newOrder = new Order
            {
                IsDeleted = false, CustomerId = 1, Status = OrderStatus.New,
                TimeStamp = new DateTime(2022, 02, 11, 13, 45, 23),
                ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 98.97m,
                OrderItems = new List<OrderItem>
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
            await _repository.AddAsync(newOrder);

            // Assert
            Assert.That(_context.Orders.Count(), Is.EqualTo(7), "The new order has not been added to the context.");
            Assert.That(await _context.Orders.FindAsync(7), Is.Not.Null, "The new order has been added with the wrong ID.");
            Assert.That(_context.OrderItems.Count(), Is.EqualTo(20), "The items of the new order has not been added to the context.");
        }

        [Test]
        public async Task UpdateAsync_ExistingOrder_UpdatesOrderAndSavesToDb()
        {
            // Arrange
            var order = await _context.Orders.FindAsync(1);

            // Act
            order.ShippingCity = "NewCity";
            await _repository.UpdateAsync(order);

            // Assert
            Assert.That((await _context.Orders.FindAsync(order.Id)).ShippingCity, Is.EqualTo("NewCity"), "The order has not been updated.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DeleteAsync_ExistingOrder_DeletesOrderAndSavesToDb(int id)
        {
            // Arrange

            // Act
            await _repository.DeleteAsync(id);

            // Assert
            Assert.That(_context.Orders.Count(), Is.EqualTo(5), "Any orders has not been deleted.");
            Assert.That(await _context.Orders.FindAsync(id), Is.Null, "The selected order has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(-1)]
        public Task DeleteAsync_NotExistingOrder_ThrowsArgumentNullException(int id)
        {
            // Arrange

            // Act
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method has not thrown the ArgumentNullException.");
            return Task.CompletedTask;
        }
    }
}