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

namespace eStore.Infrastructure.Tests.Persistence
{
    [TestFixture]
    public class MousepadRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new MousepadRepository(_context);
        }

        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Mousepad> _repository;

        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task GetByIdAsync_ExistingMousepad_ReturnsMousepad(int id)
        {
            // Arrange
            Mousepad expected = _helper.Mousepads.FirstOrDefault(c => c.Id == id);

            // Act
            Mousepad actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new MousepadEqualityComparer()),
                "The actual mousepad is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingMousepad_ReturnsNull(int id)
        {
            // Arrange

            // Act
            Mousepad actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null mousepad.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfMousepads()
        {
            // Arrange
            var expected = _helper.Mousepads;

            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new MousepadEqualityComparer()),
                "The actual collection of mousepads is not equal to the expected.");
        }

        [Test]
        public Task Query_WithPredicate_ReturnsSuitableMousepads()
        {
            // Arrange
            var expected = _helper.Mousepads.Where(c => c.Id == 13);

            // Act
            var actual = _repository.Query(c => c.Id == 13);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new MousepadEqualityComparer()),
                "The actual collection of mousepads is not equal to the expected.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddAsync_NewMousepad_AddsMousepadAndSavesToDb()
        {
            // Arrange
            var newMousepad = new Mousepad
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
            Assert.That(_context.Mousepads.Count(), Is.EqualTo(4),
                "The new mousepad has not been added to the context.");
            Assert.That(await _context.Mousepads.FindAsync(16), Is.Not.Null,
                "The new mousepad has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingMousepad_UpdatesMousepadAndSavesToDb()
        {
            // Arrange
            Mousepad mousepad = await _context.Mousepads.FindAsync(13);

            // Act
            mousepad.Name = "NewName";
            await _repository.UpdateAsync(mousepad);

            // Assert
            Assert.That((await _context.Mousepads.FindAsync(mousepad.Id)).Name, Is.EqualTo("NewName"),
                "The mousepad has not been updated.");
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
            Assert.That(_context.Mousepads.Count(), Is.EqualTo(2), "Any mousepads has not been deleted.");
            Assert.That(await _context.Mousepads.FindAsync(id), Is.Null, "The selected mousepad has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public Task DeleteAsync_NotExistingMousepad_ThrowsArgumentNullException(int id)
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