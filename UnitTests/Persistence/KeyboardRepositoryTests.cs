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
    public class KeyboardRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Keyboard> _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new KeyboardRepository(_context);
        }
        
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public async Task GetByIdAsync_ExistingKeyboard_ReturnsKeyboard(int id)
        {
            // Arrange
            var expected = _helper.Keyboards.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await _repository.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual keyboard is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingKeyboard_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null keyboard.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfKeyboards()
        {
            // Arrange
            var expected = _helper.Keyboards;
            
            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of keyboards is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableKeyboards()
        {
            // Arrange
            var expected = _helper.Keyboards.Where(c => c.Id == 5);

            // Act
            var actual = _repository.Query(c => c.Id == 5);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of keyboards is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewKeyboard_AddsKeyboardAndSavesToDb()
        {
            // Arrange
            var newKeyboard = new Keyboard()
            {
                IsDeleted = false, Name = "NewKeyboard", Created = new DateTime(2022, 01, 27, 14, 56, 20),
                LastModified = new DateTime(2022, 01, 27, 14, 56, 20),
                Description = "Description", Manufacturer = "Manufacturer5", Price = 67.99m, BigImageUrl = "big16.png",
                ThumbnailImageUrl = "thumbnail16.png",
                Length = 450, Width = 140, Height = 35, Weight = 800, Backlight = "Backlight2", Size = "Size2", Type = "Type2",
                ConnectionType = "ConnectionType2", SwitchId = 1, FrameMaterial = "Material3", KeycapMaterial = "Material2", KeyRollover = "Rollover2"
            };
            
            // Act
            await _repository.AddAsync(newKeyboard);

            // Assert
            Assert.AreEqual(6, _context.Keyboards.Count(), "The new keyboard has not been added to the context.");
            Assert.IsNotNull(await _context.Keyboards.FindAsync(16), "The new keyboard has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingKeyboard_UpdatesKeyboardAndSavesToDb()
        {
            // Arrange
            var keyboard = await _context.Keyboards.FindAsync(5);
            
            // Act
            keyboard.Name = "NewName";
            await _repository.UpdateAsync(keyboard);

            // Assert
            Assert.AreEqual("NewName", (await _context.Keyboards.FindAsync(keyboard.Id)).Name, "The keyboard has not been updated.");
        }

        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public async Task DeleteAsync_ExistingKeyboard_DeletesKeyboardAndSavesToDb(int id)
        {
            // Arrange
            
            // Act
            await _repository.DeleteAsync(id);

            // Assert
            Assert.AreEqual(4, _context.Keyboards.Count(), "Any keyboards has not been deleted.");
            Assert.IsNull(await _context.Keyboards.FindAsync(id), "The selected keyboard has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingKeyboard_ThrowsArgumentNullException(int id)
        {
            // Arrange
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}