using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Services;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Domain
{
    [TestFixture]
    public class MouseServiceTests
    {
        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        private Mock<IUnitOfWork> _mockUnitOfWork;

        [Test]
        public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfMouses()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted);

            // Act
            var actual = await service.GetPresentAsync();

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeleted_ReturnsNotDeleted()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted);
            var filterModel = new MouseFilterModel();

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleConnectionType_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && m.ConnectionTypeId == 1);
            var filterModel = new MouseFilterModel
            {
                ConnectionTypeIds = new List<int> { 1 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleConnectionTypes_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Mouses.Where(m => !m.IsDeleted && (m.ConnectionTypeId == 1 || m.ConnectionTypeId == 2));
            var filterModel = new MouseFilterModel
            {
                ConnectionTypeIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleBacklight_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && m.BacklightId == 1);
            var filterModel = new MouseFilterModel
            {
                BacklightIds = new List<int> { 1 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleBacklights_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && (m.BacklightId == 1 || m.BacklightId == 2));
            var filterModel = new MouseFilterModel
            {
                BacklightIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }


        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleManufacturer_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && m.ManufacturerId == 3);
            var filterModel = new MouseFilterModel
            {
                ManufacturerIds = new List<int> { 3 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleManufacturers_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Mouses.Where(m => !m.IsDeleted && (m.ManufacturerId == 3 || m.ManufacturerId == 4));
            var filterModel = new MouseFilterModel
            {
                ManufacturerIds = new List<int> { 3, 4 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMinPrice_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && m.Price >= 34.99m);
            var filterModel = new MouseFilterModel
            {
                MinPrice = 34.99m
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMaxPrice_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && m.Price <= 34.99m);
            var filterModel = new MouseFilterModel
            {
                MaxPrice = 34.99m
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndPrice_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(g => !g.IsDeleted && g.Price >= 34.99m && g.Price <= 44.99m);
            var filterModel = new MouseFilterModel
            {
                MinPrice = 34.99m,
                MaxPrice = 44.99m
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMinWeight_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && m.Weight >= 70);
            var filterModel = new MouseFilterModel
            {
                MinWeight = 70
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMaxWeight_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && m.Weight <= 70);
            var filterModel = new MouseFilterModel
            {
                MaxWeight = 70
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndWeight_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => UnitTestHelper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mouses.Where(m => !m.IsDeleted && m.Weight >= 70 && m.Weight <= 83);
            var filterModel = new MouseFilterModel
            {
                MinWeight = 70,
                MaxWeight = 83
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_ExistingMouse_ReturnsMouse()
        {
            // Arrange
            var expected = UnitTestHelper.Mouses.First(g => g.Id == 11);
            _mockUnitOfWork.Setup(x => x.MouseRepository.GetByIdAsync(11)).ReturnsAsync(expected);
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(11);

            // Assert
            Assert.AreEqual(expected, actual, "The actual mouse is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_NotExistingGamepad_ReturnsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.GetByIdAsync(11)).ReturnsAsync((Mouse)null);
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(1);

            // Assert
            Assert.IsNull(actual, "The method returned not-null object.");
        }

        [Test]
        public async Task GetManufacturersAsync_NotEmptyDb_ReturnsCollectionOfManufacturers()
        {
            // Arrange
            var mouses = UnitTestHelper.Mouses.ToList();
            foreach (var mouse in mouses)
                mouse.Manufacturer = UnitTestHelper.Manufacturers.First(m => m.Id == mouse.ManufacturerId);

            _mockUnitOfWork.Setup(x => x.MouseRepository.GetAllAsync()).ReturnsAsync(mouses);
            IMouseService service = new MouseService(_mockUnitOfWork.Object);
            var expected = mouses.Select(m => m.Manufacturer).Distinct();

            // Act
            var actual = await service.GetManufacturersAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equivalent to expected.");
        }

        [Test]
        public async Task GetConnectionTypesAsync_NotEmptyDb_ReturnsCollectionOfConnectionTypes()
        {
            // Arrange
            var mouses = UnitTestHelper.Mouses.ToList();
            foreach (var mouse in mouses)
                mouse.ConnectionType = UnitTestHelper.ConnectionTypes.First(t => t.Id == mouse.ConnectionTypeId);

            _mockUnitOfWork.Setup(x => x.MouseRepository.GetAllAsync()).ReturnsAsync(mouses);
            IMouseService service = new MouseService(_mockUnitOfWork.Object);
            var expected = mouses.Select(m => m.ConnectionType).Distinct();

            // Act
            var actual = await service.GetConnectionTypesAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetBacklightsAsync_NotEmptyDb_ReturnsCollectionOfBacklights()
        {
            // Arrange
            var mouses = UnitTestHelper.Mouses.ToList();
            foreach (var mouse in mouses)
                mouse.Backlight = UnitTestHelper.Backlights.First(b => b.Id == mouse.BacklightId);

            _mockUnitOfWork.Setup(x => x.MouseRepository.GetAllAsync()).ReturnsAsync(mouses);
            IMouseService service = new MouseService(_mockUnitOfWork.Object);
            var expected = mouses.Select(m => m.Backlight).Distinct();

            // Act
            var actual = await service.GetBacklightsAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }
    }
}