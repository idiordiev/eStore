using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.Infrastructure.Data;
using eStore.Infrastructure.Data.Repositories;
using NUnit.Framework;

namespace eStore.UnitTests.Data
{
    [TestFixture]
    public class MousepadRepositoryTests
    {
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task GetByIdAsync_ExistingMousepad_ReturnsMousepad(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mousepad> repo = new MousepadRepository(context);
            var expected = UnitTestHelper.Mousepads.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await repo.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual mousepad is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingMousepad_ReturnsNull(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mousepad> repo = new MousepadRepository(context);

            // Act
            var actual = await repo.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null mousepad.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfMousepads()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mousepad> repo = new MousepadRepository(context);
            var expected = UnitTestHelper.Mousepads;
            
            // Act
            var actual = await repo.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of mousepads is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableMousepads()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mousepad> repo = new MousepadRepository(context);
            var expected = UnitTestHelper.Mousepads.Where(c => c.Id == 13);

            // Act
            var actual = repo.Query(c => c.Id == 13);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of mousepads is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewMousepad_AddsMousepadAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mousepad> repo = new MousepadRepository(context);
            var newMousepad = new Mousepad()
            {
                IsDeleted = false, Name = "NewMousepad", Created = new DateTime(2022, 01, 25, 14, 40, 20),
                LastModified = new DateTime(2022, 01, 25, 14, 40, 20),
                Description = "Description", ManufacturerId = 4, Price = 23.99m, BigImageUrl = "big16.png",
                ThumbnailImageUrl = "thumbnail16.png", Length = 320, Width = 270, Height = 3, IsStitched = false, 
                BacklightId = 1, BottomMaterialId = 4, TopMaterialId = 5
            };
            
            // Act
            await repo.AddAsync(newMousepad);

            // Assert
            Assert.AreEqual(4, context.Mousepads.Count(), "The new mousepad has not been added to the context.");
            Assert.IsNotNull(await context.Mousepads.FindAsync(16), "The new mousepad has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingMousepad_UpdatesMousepadAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mousepad> repo = new MousepadRepository(context);
            var mousepad = await context.Mousepads.FindAsync(13);
            
            // Act
            mousepad.Name = "NewName";
            await repo.UpdateAsync(mousepad);

            // Assert
            Assert.AreEqual("NewName", (await context.Mousepads.FindAsync(mousepad.Id)).Name, "The mousepad has not been updated.");
        }

        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task DeleteAsync_ExistingMousepad_DeletesMousepadAndSavesToDb(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mousepad> repo = new MousepadRepository(context);
            
            // Act
            await repo.DeleteAsync(id);

            // Assert
            Assert.AreEqual(2, context.Mousepads.Count(), "Any mousepads has not been deleted.");
            Assert.IsNull(await context.Mousepads.FindAsync(id), "The selected mousepad has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingMousepad_ThrowsArgumentNullException(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mousepad> repo = new MousepadRepository(context);
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await repo.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}