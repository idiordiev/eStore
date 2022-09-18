using System;
using System.Collections.Generic;
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
    public class GoodsRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task GetByIdAsync_ExistingGoods_ReturnsGoods(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Goods> repo = new GoodsRepository(context);
            var expected = UnitTestHelper.Goods.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await repo.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual goods is not equal to the expected.");
        }

        [TestCase(16)]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingGoods_ReturnsNull(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Goods> repo = new GoodsRepository(context);

            // Act
            var actual = await repo.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null goods.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfGoods()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Goods> repo = new GoodsRepository(context);
            IEnumerable<Goods> expected = UnitTestHelper.Goods;
            
            // Act
            var actual = await repo.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of goodss is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableGoods()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Goods> repo = new GoodsRepository(context);
            var expected = UnitTestHelper.Goods.Where(c => c.Id == 13);

            // Act
            var actual = repo.Query(c => c.Id == 13);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of goods is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewMouse_AddsGoodsAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Goods> repo = new GoodsRepository(context);
            var newGoods = new Mouse()
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
            await repo.AddAsync(newGoods);

            // Assert
            Assert.AreEqual(16, context.Goods.Count(), "The new goods has not been added to the context.");
            Assert.IsNotNull(await context.Goods.FindAsync(16), "The new goods has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingGoods_UpdatesGoodsAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Goods> repo = new GoodsRepository(context);
            var goods = await context.Goods.FindAsync(13);
            
            // Act
            goods.Name = "NewName";
            await repo.UpdateAsync(goods);

            // Assert
            Assert.AreEqual("NewName", (await context.Goods.FindAsync(goods.Id)).Name, "The goods has not been updated.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task DeleteAsync_ExistingGoods_DeletesGoodsAndSavesToDb(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Goods> repo = new GoodsRepository(context);
            
            // Act
            await repo.DeleteAsync(id);

            // Assert
            Assert.AreEqual(14, context.Goods.Count(), "Any goods has not been deleted.");
            Assert.IsNull(await context.Goods.FindAsync(id), "The selected goods has not been deleted.");
        }

        [TestCase(16)]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingGoods_ThrowsArgumentNullException(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Goods> repo = new GoodsRepository(context);
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await repo.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}