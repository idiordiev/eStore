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
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public async Task GetByIdAsync_ExistingKeyboard_ReturnsKeyboard(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Keyboard> repo = new KeyboardRepository(context);
            var expected = UnitTestHelper.Keyboards.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await repo.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual keyboard is not equal to the expected.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingKeyboard_ReturnsNull(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Keyboard> repo = new KeyboardRepository(context);

            // Act
            var actual = await repo.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null keyboard.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfKeyboards()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Keyboard> repo = new KeyboardRepository(context);
            var expected = UnitTestHelper.Keyboards;
            
            // Act
            var actual = await repo.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of keyboards is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableKeyboards()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Keyboard> repo = new KeyboardRepository(context);
            var expected = UnitTestHelper.Keyboards.Where(c => c.Id == 5);

            // Act
            var actual = repo.Query(c => c.Id == 5);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of keyboards is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewKeyboard_AddsKeyboardAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Keyboard> repo = new KeyboardRepository(context);
            var newKeyboard = new Keyboard()
            {
                IsDeleted = false, Name = "NewKeyboard", Created = new DateTime(2022, 01, 27, 14, 56, 20),
                LastModified = new DateTime(2022, 01, 27, 14, 56, 20),
                Description = "Description", ManufacturerId = 5, Price = 67.99m, BigImageUrl = "big16.png",
                ThumbnailImageUrl = "thumbnail16.png",
                Length = 450, Width = 140, Height = 35, Weight = 800, BacklightId = 2, SizeId = 2, TypeId = 2,
                ConnectionTypeId = 2, SwitchId = 1, FrameMaterialId = 3, KeycapMaterialId = 2, KeyRolloverId = 2
            };
            
            // Act
            await repo.AddAsync(newKeyboard);

            // Assert
            Assert.AreEqual(6, context.Keyboards.Count(), "The new keyboard has not been added to the context.");
            Assert.IsNotNull(await context.Keyboards.FindAsync(16), "The new keyboard has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingKeyboard_UpdatesKeyboardAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Keyboard> repo = new KeyboardRepository(context);
            var keyboard = await context.Keyboards.FindAsync(5);
            
            // Act
            keyboard.Name = "NewName";
            await repo.UpdateAsync(keyboard);

            // Assert
            Assert.AreEqual("NewName", (await context.Keyboards.FindAsync(keyboard.Id)).Name, "The keyboard has not been updated.");
        }

        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public async Task DeleteAsync_ExistingKeyboard_DeletesKeyboardAndSavesToDb(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Keyboard> repo = new KeyboardRepository(context);
            
            // Act
            await repo.DeleteAsync(id);

            // Assert
            Assert.AreEqual(4, context.Keyboards.Count(), "Any keyboards has not been deleted.");
            Assert.IsNull(await context.Keyboards.FindAsync(id), "The selected keyboard has not been deleted.");
        }

        [TestCase(12)]
        [TestCase(1)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingKeyboard_ThrowsArgumentNullException(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Keyboard> repo = new KeyboardRepository(context);
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await repo.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}