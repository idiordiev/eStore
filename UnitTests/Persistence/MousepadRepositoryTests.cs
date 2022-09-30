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
    public class MousepadRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Mousepad> _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new MousepadRepository(_context);
        }
        
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task GetByIdAsync_ExistingMousepad_ReturnsMousepad(int id)
        {
            // Arrange
            var expected = _helper.Mousepads.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await _repository.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual mousepad is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingMousepad_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null mousepad.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfMousepads()
        {
            // Arrange
            var expected = _helper.Mousepads;
            
            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of mousepads is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableMousepads()
        {
            // Arrange
            var expected = _helper.Mousepads.Where(c => c.Id == 13);

            // Act
            var actual = _repository.Query(c => c.Id == 13);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of mousepads is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewMousepad_AddsMousepadAndSavesToDb()
        {
            // Arrange
            var newMousepad = new Mousepad()
            {
                IsDeleted = false, Name = "NewMousepad", Created = new DateTime(2022, 01, 25, 14, 40, 20),
                LastModified = new DateTime(2022, 01, 25, 14, 40, 20),
                Description = "Description", Manufacturer = "Manufacturer4", Price = 23.99m, BigImageUrl = "big16.png",
                ThumbnailImageUrl = "thumbnail16.png", Length = 320, Width = 270, Height = 3, IsStitched = false, 
                Backlight = "Backlight1", BottomMaterial = "Material4", TopMaterial = "Material5"
            };
            
            // Act
            await _repository.AddAsync(newMousepad);

            // Assert
            Assert.AreEqual(4, _context.Mousepads.Count(), "The new mousepad has not been added to the context.");
            Assert.IsNotNull(await _context.Mousepads.FindAsync(16), "The new mousepad has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingMousepad_UpdatesMousepadAndSavesToDb()
        {
            // Arrange
            var mousepad = await _context.Mousepads.FindAsync(13);
            
            // Act
            mousepad.Name = "NewName";
            await _repository.UpdateAsync(mousepad);

            // Assert
            Assert.AreEqual("NewName", (await _context.Mousepads.FindAsync(mousepad.Id)).Name, "The mousepad has not been updated.");
        }

        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task DeleteAsync_ExistingMousepad_DeletesMousepadAndSavesToDb(int id)
        {
            // Arrange
            
            // Act
            await _repository.DeleteAsync(id);

            // Assert
            Assert.AreEqual(2, _context.Mousepads.Count(), "Any mousepads has not been deleted.");
            Assert.IsNull(await _context.Mousepads.FindAsync(id), "The selected mousepad has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingMousepad_ThrowsArgumentNullException(int id)
        {
            // Arrange
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}