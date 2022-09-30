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
    public class MouseRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Mouse> _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new MouseRepository(_context);
        }
        
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
            Assert.AreEqual(expected, actual, "The actual mouse is not equal to the expected.");
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
            Assert.IsNull(actual, "The method returned not-null mouse.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfMouses()
        {
            // Arrange
            var expected = _helper.Mouses;
            
            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of mouses is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableMouses()
        {
            // Arrange
            var expected = _helper.Mouses.Where(c => c.Id == 13);

            // Act
            var actual = _repository.Query(c => c.Id == 13);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of mouses is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewMouse_AddsMouseAndSavesToDb()
        {
            // Arrange
            var newMouse = new Mouse()
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
            Assert.AreEqual(4, _context.Mouses.Count(), "The new mouse has not been added to the context.");
            Assert.IsNotNull(await _context.Mouses.FindAsync(16), "The new mouse has been added with the wrong ID.");
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
            Assert.AreEqual("NewName", (await _context.Mouses.FindAsync(mouse.Id)).Name, "The mouse has not been updated.");
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
            Assert.AreEqual(2, _context.Mouses.Count(), "Any mouses has not been deleted.");
            Assert.IsNull(await _context.Mouses.FindAsync(id), "The selected mouse has not been deleted.");
        }

        [TestCase(13)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingMouse_ThrowsArgumentNullException(int id)
        {
            // Arrange
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}