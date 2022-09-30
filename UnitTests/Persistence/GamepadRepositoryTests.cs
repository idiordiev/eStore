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
    public class GamepadRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Gamepad> _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new GamepadRepository(_context);
        }
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task GetByIdAsync_ExistingGamepad_ReturnsGamepad(int id)
        {
            // Arrange
            var expected = _helper.Gamepads.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await _repository.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual gamepad is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingGamepad_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null gamepad.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfGamepads()
        {
            // Arrange
            var expected = _helper.Gamepads;
            
            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of gamepads is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableGamepads()
        {
            // Arrange
            var expected = _helper.Gamepads.Where(c => c.Id == 2);

            // Act
            var actual = _repository.Query(c => c.Id == 2);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of gamepads is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewGamepad_AddsGamepadAndSavesToDb()
        {
            // Arrange
            var newGamepad = new Gamepad()
            {
                IsDeleted = false, Name = "NewGamepad", Created = new DateTime(2022, 01, 26, 14, 36, 20),
                Description = "Description", Manufacturer = "Manufacturer3", Price = 44.99m,
                LastModified = new DateTime(2022, 01, 26, 14, 36, 20),
                ConnectionType = "ConnectionType1", Weight = 260, Feedback = "Feedback1", BigImageUrl = "big16.png",
                ThumbnailImageUrl = "thumbnail16.png"
            };
            
            // Act
            await _repository.AddAsync(newGamepad);

            // Assert
            Assert.AreEqual(5, _context.Gamepads.Count(), "The new gamepad has not been added to the context.");
            Assert.IsNotNull(await _context.Gamepads.FindAsync(16), "The new gamepad has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingGamepad_UpdatesGamepadAndSavesToDb()
        {
            // Arrange
            var gamepad = await _context.Gamepads.FindAsync(1);
            
            // Act
            gamepad.Name = "Name1";
            await _repository.UpdateAsync(gamepad);

            // Assert
            Assert.AreEqual("Name1", (await _context.Gamepads.FindAsync(gamepad.Id)).Name, "The gamepad has not been updated.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DeleteAsync_ExistingGamepad_DeletesGamepadAndSavesToDb(int id)
        {
            // Arrange
            
            // Act
            await _repository.DeleteAsync(id);

            // Assert
            Assert.AreEqual(3, _context.Gamepads.Count(), "Any gamepads has not been deleted.");
            Assert.IsNull(await _context.Gamepads.FindAsync(id), "The selected gamepad has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingGamepad_ThrowsArgumentNullException(int id)
        {
            // Arrange
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}