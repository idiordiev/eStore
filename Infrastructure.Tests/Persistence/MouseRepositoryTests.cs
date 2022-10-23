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
    public class MouseRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new MouseRepository(_context);
        }

        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Mouse> _repository;

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task GetByIdAsync_ExistingMouse_ReturnsMouse(int id)
        {
            // Arrange
            var expected = _helper.Mouses.FirstOrDefault(c => c.Id == id);

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual mouse is not equal to the expected.");
        }

        [TestCase(13)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingMouse_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null mouse.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfMouses()
        {
            // Arrange
            var expected = _helper.Mouses;

            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual collection of mouses is not equal to the expected.");
        }

        [Test]
        public Task Query_WithPredicate_ReturnsSuitableMouses()
        {
            // Arrange
            var expected = _helper.Mouses.Where(c => c.Id == 13);

            // Act
            var actual = _repository.Query(c => c.Id == 13);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual collection of mouses is not equal to the expected.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddAsync_NewMouse_AddsMouseAndSavesToDb()
        {
            // Arrange
            var newMouse = new Mouse
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
            await _repository.AddAsync(newMouse);

            // Assert
            Assert.That(_context.Mouses.Count(), Is.EqualTo(4), "The new mouse has not been added to the context.");
            Assert.That(await _context.Mouses.FindAsync(16), Is.Not.Null, "The new mouse has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingMouse_UpdatesMouseAndSavesToDb()
        {
            // Arrange
            var mouse = await _context.Mouses.FindAsync(12);

            // Act
            mouse.Name = "NewName";
            await _repository.UpdateAsync(mouse);

            // Assert
            Assert.That((await _context.Mouses.FindAsync(mouse.Id)).Name, Is.EqualTo("NewName"), "The mouse has not been updated.");
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task DeleteAsync_ExistingMouse_DeletesMouseAndSavesToDb(int id)
        {
            // Arrange

            // Act
            await _repository.DeleteAsync(id);

            // Assert
            Assert.That(_context.Mouses.Count(), Is.EqualTo(2), "Any mouses has not been deleted.");
            Assert.That(await _context.Mouses.FindAsync(id), Is.Null, "The selected mouse has not been deleted.");
        }

        [TestCase(13)]
        [TestCase(1)]
        [TestCase(-1)]
        public Task DeleteAsync_NotExistingMouse_ThrowsArgumentNullException(int id)
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