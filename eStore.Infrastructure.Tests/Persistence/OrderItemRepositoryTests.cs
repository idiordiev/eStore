using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Persistence.Repositories;
using eStore.Tests.Common;
using eStore.Tests.Common.EqualityComparers;
using NUnit.Framework;

namespace eStore.Infrastructure.Tests.Persistence;

[TestFixture]
public class OrderItemRepositoryTests
{
    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _context = _helper.GetApplicationContext();
        _repository = new OrderItemRepository(_context);
    }

    private UnitTestHelper _helper;
    private ApplicationContext _context;
    private IRepository<OrderItem> _repository;

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
        OrderItem expected = _helper.OrderItems.FirstOrDefault(c => c.Id == id);

        // Act
        OrderItem actual = await _repository.GetByIdAsync(id);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new OrderItemEqualityComparer()),
            "The actual order item is not equal to the expected.");
    }

    [TestCase(19)]
    [TestCase(-1)]
    public async Task GetByIdAsync_NotExistingOrderItem_ReturnsNull(int id)
    {
        // Arrange

        // Act
        OrderItem actual = await _repository.GetByIdAsync(id);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null order item.");
    }

    [Test]
    public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfOrderItems()
    {
        // Arrange
        var expected = _helper.OrderItems;

        // Act
        var actual = await _repository.GetAllAsync();

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new OrderItemEqualityComparer()),
            "The actual collection of order items is not equal to the expected.");
    }

    [Test]
    public async Task Query_WithPredicate_ReturnsSuitableOrderItems()
    {
        // Arrange
        var expected = _helper.OrderItems.Where(c => c.Id == 13);

        // Act
        var actual = await _repository.QueryAsync(c => c.Id == 13);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new OrderItemEqualityComparer()),
            "The actual collection of order items is not equal to the expected.");
    }

    [Test]
    public async Task AddAsync_NewOrderItem_AddsOrderItemAndSavesToDb()
    {
        // Arrange
        var newOrderItem = new OrderItem
        {
            IsDeleted = false, OrderId = 4, GoodsId = 2, Quantity = 1, UnitPrice = 24.99m
        };

        // Act
        await _repository.AddAsync(newOrderItem);

        // Assert
        Assert.That(_context.OrderItems.Count(), Is.EqualTo(19),
            "The new order item has not been added to the context.");
        Assert.That(await _context.OrderItems.FindAsync(19), Is.Not.Null,
            "The new order item has been added with the wrong ID.");
    }

    [Test]
    public async Task UpdateAsync_ExistingOrderItem_UpdatesOrderItemAndSavesToDb()
    {
        // Arrange
        OrderItem orderItem = await _context.OrderItems.FindAsync(12);

        // Act
        orderItem.OrderId = 4;
        await _repository.UpdateAsync(orderItem);

        // Assert
        Assert.That((await _context.OrderItems.FindAsync(orderItem.Id)).OrderId, Is.EqualTo(4),
            "The order item has not been updated.");
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
        Assert.That(_context.OrderItems.Count(), Is.EqualTo(17), "Any order items has not been deleted.");
        Assert.That(await _context.OrderItems.FindAsync(id), Is.Null,
            "The selected order item has not been deleted.");
    }

    [TestCase(19)]
    [TestCase(-1)]
    public Task DeleteAsync_NotExistingOrderItem_ThrowsArgumentNullException(int id)
    {
        // Arrange

        // Act
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

        // Assert
        Assert.That(exception, Is.Not.Null, "The method has not thrown the ArgumentNullException.");
        return Task.CompletedTask;
    }
    
    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}