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
    public class GamepadRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task GetByIdAsync_ExistingGamepad_ReturnsGamepad(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Gamepad> repo = new GamepadRepository(context);
            var expected = UnitTestHelper.Gamepads.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await repo.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual gamepad is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingGamepad_ReturnsNull(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Gamepad> repo = new GamepadRepository(context);

            // Act
            var actual = await repo.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null gamepad.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfGamepads()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Gamepad> repo = new GamepadRepository(context);
            var expected = UnitTestHelper.Gamepads;
            
            // Act
            var actual = await repo.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of gamepads is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableGamepads()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Gamepad> repo = new GamepadRepository(context);
            var expected = UnitTestHelper.Gamepads.Where(c => c.Id == 2);

            // Act
            var actual = repo.Query(c => c.Id == 2);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of gamepads is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewGamepad_AddsGamepadAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Gamepad> repo = new GamepadRepository(context);
            var newGamepad = new Gamepad()
            {
                IsDeleted = false, Name = "NewGamepad", Created = new DateTime(2022, 01, 26, 14, 36, 20),
                Description = "Description", ManufacturerId = 3, Price = 44.99m,
                LastModified = new DateTime(2022, 01, 26, 14, 36, 20),
                ConnectionTypeId = 1, Weight = 260, FeedbackId = 1, BigImageUrl = "big16.png",
                ThumbnailImageUrl = "thumbnail16.png"
            };
            
            // Act
            await repo.AddAsync(newGamepad);

            // Assert
            Assert.AreEqual(5, context.Gamepads.Count(), "The new gamepad has not been added to the context.");
            Assert.IsNotNull(await context.Gamepads.FindAsync(16), "The new gamepad has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingGamepad_UpdatesGamepadAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Gamepad> repo = new GamepadRepository(context);
            var gamepad = await context.Gamepads.FindAsync(1);
            
            // Act
            gamepad.Name = "Name1";
            await repo.UpdateAsync(gamepad);

            // Assert
            Assert.AreEqual("Name1", (await context.Gamepads.FindAsync(gamepad.Id)).Name, "The gamepad has not been updated.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DeleteAsync_ExistingGamepad_DeletesGamepadAndSavesToDb(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Gamepad> repo = new GamepadRepository(context);
            
            // Act
            await repo.DeleteAsync(id);

            // Assert
            Assert.AreEqual(3, context.Gamepads.Count(), "Any gamepads has not been deleted.");
            Assert.IsNull(await context.Gamepads.FindAsync(id), "The selected gamepad has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingGamepad_ThrowsArgumentNullException(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Gamepad> repo = new GamepadRepository(context);
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await repo.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}