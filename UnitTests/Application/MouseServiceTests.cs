using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.FilterModels;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Application.Services;
using eStore.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Application
{
    [TestFixture]
    public class MouseServiceTests
    {
        private UnitTestHelper _helper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfMouses()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted);

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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted);
            var filterModel = new MouseFilterModel();

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("ConnectionType1")]
        [TestCase("ConneCtionType1")]
        [TestCase("connectiontype1")]
        [TestCase("ConnectionType1111")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleConnectionType_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && m.ConnectionType.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new MouseFilterModel
            {
                ConnectionTypes = new List<string> { paramValue }
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected =
                _helper.Mouses.Where(m => !m.IsDeleted && (m.ConnectionType.Equals("ConnectionType1", StringComparison.InvariantCultureIgnoreCase) || m.ConnectionType.Equals("ConnectionType2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new MouseFilterModel
            {
                ConnectionTypes = new List<string> { "ConnectionType1", "ConnectionType2" }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Backlight2")]
        [TestCase("BAcklighT2")]
        [TestCase("BackLiGhT2")]
        [TestCase("backlight2")]
        [TestCase("Backlight2222")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleBacklight_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && m.Backlight.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new MouseFilterModel
            {
                Backlights = new List<string> { paramValue }
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && (m.Backlight.Equals("Backlight1", StringComparison.InvariantCultureIgnoreCase) || m.Backlight.Equals("Backlight2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new MouseFilterModel
            {
                Backlights = new List<string> { "Backlight1", "Backlight2" }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }
        
        [TestCase("Manufacturer3")]
        [TestCase("ManUfacturer3")]
        [TestCase("mAnUfAcTuReR3")]
        [TestCase("manufacturer3")]
        [TestCase("Manufacturer333")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleManufacturer_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MouseRepository.Query(It.IsAny<Expression<Func<Mouse, bool>>>()))
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && m.Manufacturer.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new MouseFilterModel
            {
                Manufacturers = new List<string> { paramValue }
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected =
                _helper.Mouses.Where(m => !m.IsDeleted && (m.Manufacturer.Equals("Manufacturer3", StringComparison.InvariantCultureIgnoreCase) || m.Manufacturer.Equals("Manufacturer4", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new MouseFilterModel
            {
                Manufacturers = new List<string> { "Manufacturer3", "Manufacturer4" }
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && m.Price >= 34.99m);
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && m.Price <= 34.99m);
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(g => !g.IsDeleted && g.Price >= 34.99m && g.Price <= 44.99m);
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && m.Weight >= 70);
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && m.Weight <= 70);
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
                .Returns((Expression<Func<Mouse, bool>> predicate) => _helper.Mouses.Where(predicate.Compile()));
            IMouseService service = new MouseService(_mockUnitOfWork.Object);

            var expected = _helper.Mouses.Where(m => !m.IsDeleted && m.Weight >= 70 && m.Weight <= 83);
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
            var expected = _helper.Mouses.First(g => g.Id == 11);
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
            var mouses = _helper.Mouses.ToList();
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
            var mouses = _helper.Mouses.ToList();
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
            var mouses = _helper.Mouses.ToList();
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