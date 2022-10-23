using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Persistence.Repositories;
using eStore.Tests.Common;
using NUnit.Framework;

namespace eStore.Infrastructure.Tests.Persistence
{
    [TestFixture]
    public class GoodsRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new GoodsRepository(_context);
        }

        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Goods> _repository;

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
        [TestCase(15)]
        public async Task GetByIdAsync_ExistingGoods_ReturnsGoods(int id)
        {
            // Arrange
            var expected = _helper.Goods.FirstOrDefault(c => c.Id == id);

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual goods is not equal to the expected.");
        }

        [TestCase(16)]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingGoods_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null goods.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfGoods()
        {
            // Arrange
            var expected = _helper.Goods;

            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual collection of goods is not equal to the expected.");
        }

        [Test]
        public Task Query_WithPredicate_ReturnsSuitableGoods()
        {
            // Arrange
            var expected = _helper.Goods.Where(c => c.Id == 13);

            // Act
            var actual = _repository.Query(c => c.Id == 13);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual collection of goods is not equal to the expected.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddAsync_NewMouse_AddsGoodsAndSavesToDb()
        {
            // Arrange
            var newGoods = new Mouse
            {
                IsDeleted = false, Name = "NewMouse", Created = new DateTime(2022, 01, 25, 14, 47, 20),
                LastModified = new DateTime(2022, 01, 25, 14, 47, 20),
                Description = "Description", Manufacturer = "Manufacturer3", Price = 37.99m, BigImageUrl = "big16.png",
                ThumbnailImageUrl = "thumbnail16.png",
                Length = 132, Width = 68, Height = 41, Weight = 73,
                Backlight = "Backlight1", ButtonsQuantity = 5, SensorName = "Sensor", MinSensorDPI = 200,
                MaxSensorDPI = 21000, ConnectionType = "ConnectionType1"
            };

            // Act
            await _repository.AddAsync(newGoods);

            // Assert
            Assert.That(_context.Goods.Count(), Is.EqualTo(16), "The new goods has not been added to the context.");
            Assert.That(await _context.Goods.FindAsync(16), Is.Not.Null, "The new goods has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingGoods_UpdatesGoodsAndSavesToDb()
        {
            // Arrange
            var goods = await _context.Goods.FindAsync(13);

            // Act
            goods.Name = "NewName";
            await _repository.UpdateAsync(goods);

            // Assert
            Assert.That((await _context.Goods.FindAsync(goods.Id)).Name, Is.EqualTo("NewName"), "The goods has not been updated.");
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
        [TestCase(15)]
        public async Task DeleteAsync_ExistingGoods_DeletesGoodsAndSavesToDb(int id)
        {
            // Arrange

            // Act
            await _repository.DeleteAsync(id);

            // Assert
            Assert.That(_context.Goods.Count(), Is.EqualTo(14), "Any goods has not been deleted.");
            Assert.That(await _context.Goods.FindAsync(id), Is.Null, "The selected goods has not been deleted.");
        }

        [TestCase(16)]
        [TestCase(0)]
        [TestCase(-1)]
        public Task DeleteAsync_NotExistingGoods_ThrowsArgumentNullException(int id)
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