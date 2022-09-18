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
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task GetByIdAsync_ExistingMouse_ReturnsMouse(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mouse> repo = new MouseRepository(context);
            var expected = UnitTestHelper.Mouses.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await repo.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual mouse is not equal to the expected.");
        }

        [TestCase(13)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingMouse_ReturnsNull(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mouse> repo = new MouseRepository(context);

            // Act
            var actual = await repo.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null mouse.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfMouses()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mouse> repo = new MouseRepository(context);
            var expected = UnitTestHelper.Mouses;
            
            // Act
            var actual = await repo.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of mouses is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableMouses()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mouse> repo = new MouseRepository(context);
            var expected = UnitTestHelper.Mouses.Where(c => c.Id == 13);

            // Act
            var actual = repo.Query(c => c.Id == 13);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of mouses is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewMouse_AddsMouseAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mouse> repo = new MouseRepository(context);
            var newMouse = new Mouse()
            {
                IsDeleted = false, Name = "NewMouse", Created = new DateTime(2022, 01, 25, 14, 47, 20),
                LastModified = new DateTime(2022, 01, 25, 14, 47, 20),
                Description = "Description", ManufacturerId = 3, Price = 37.99m, BigImageUrl = "big16.png",
                ThumbnailImageUrl = "thumbnail16.png",
                Length = 132, Width = 68, Height = 41, Weight = 73,
                BacklightId = 1, ButtonsQuantity = 5, SensorName = "Sensor", MinSensorDPI = 200,
                MaxSensorDPI = 21000, ConnectionTypeId = 1
            };
            
            // Act
            await repo.AddAsync(newMouse);

            // Assert
            Assert.AreEqual(4, context.Mouses.Count(), "The new mouse has not been added to the context.");
            Assert.IsNotNull(await context.Mouses.FindAsync(16), "The new mouse has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingMouse_UpdatesMouseAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mouse> repo = new MouseRepository(context);
            var mouse = await context.Mouses.FindAsync(12);
            
            // Act
            mouse.Name = "NewName";
            await repo.UpdateAsync(mouse);

            // Assert
            Assert.AreEqual("NewName", (await context.Mouses.FindAsync(mouse.Id)).Name, "The mouse has not been updated.");
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task DeleteAsync_ExistingMouse_DeletesMouseAndSavesToDb(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mouse> repo = new MouseRepository(context);
            
            // Act
            await repo.DeleteAsync(id);

            // Assert
            Assert.AreEqual(2, context.Mouses.Count(), "Any mouses has not been deleted.");
            Assert.IsNull(await context.Mouses.FindAsync(id), "The selected mouse has not been deleted.");
        }

        [TestCase(13)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingMouse_ThrowsArgumentNullException(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Mouse> repo = new MouseRepository(context);
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await repo.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}