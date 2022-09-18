using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.FilterModels;
using eStore.Application.Interfaces;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Application.Services;
using eStore.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Application
{
    [TestFixture]
    public class MousepadServiceTests
    {
        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        private Mock<IUnitOfWork> _mockUnitOfWork;

        [Test]
        public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfMousepads()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted);

            // Act
            var actual = await service.GetPresentAsync();

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_OnlyNotDeleted_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted);
            var filterModel = new MousepadFilterModel();

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleBottomMaterial_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && m.BottomMaterialId == 1);
            var filterModel = new MousepadFilterModel
            {
                BottomMaterialIds = new List<int> { 1 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleBottomMaterials_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m =>
                !m.IsDeleted && (m.BottomMaterialId == 1 || m.BottomMaterialId == 2));
            var filterModel = new MousepadFilterModel
            {
                BottomMaterialIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleTopMaterial_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && m.TopMaterialId == 1);
            var filterModel = new MousepadFilterModel
            {
                TopMaterialIds = new List<int> { 1 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleTopMaterials_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && (m.TopMaterialId == 1 || m.TopMaterialId == 2));
            var filterModel = new MousepadFilterModel
            {
                TopMaterialIds = new List<int> { 1, 2 }
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && m.BacklightId == 1);
            var filterModel = new MousepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && (m.BacklightId == 1 || m.BacklightId == 2));
            var filterModel = new MousepadFilterModel
            {
                BacklightIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndNotStitched_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && !m.IsStitched);
            var filterModel = new MousepadFilterModel
            {
                IsStitchedValues = new List<bool> { false }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndStitched_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && m.IsStitched);
            var filterModel = new MousepadFilterModel
            {
                IsStitchedValues = new List<bool> { true }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleIsStitchedValues_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && (m.IsStitched || !m.IsStitched));
            var filterModel = new MousepadFilterModel
            {
                IsStitchedValues = new List<bool> { true, false }
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && m.ManufacturerId == 4);
            var filterModel = new MousepadFilterModel
            {
                ManufacturerIds = new List<int> { 4 }
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && (m.ManufacturerId == 4 || m.ManufacturerId == 5));
            var filterModel = new MousepadFilterModel
            {
                ManufacturerIds = new List<int> { 4, 5 }
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && m.Price >= 45.99m);
            var filterModel = new MousepadFilterModel
            {
                MinPrice = 45.99m
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && m.Price <= 45.99m);
            var filterModel = new MousepadFilterModel
            {
                MaxPrice = 45.99m
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted && m.Price >= 45.99m && m.Price <= 48.99m);
            var filterModel = new MousepadFilterModel
            {
                MinPrice = 45.99m,
                MaxPrice = 48.99m
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndAllOtherParameters_ReturnsFirst()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    UnitTestHelper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Mousepads.Where(m => !m.IsDeleted);
            var filterModel = new MousepadFilterModel
            {
                IsStitchedValues = new List<bool> { true, false },
                BottomMaterialIds = new List<int> { 1, 2, 3, 4, 5 },
                TopMaterialIds = new List<int> { 1, 2, 3, 4, 5 },
                ManufacturerIds = new List<int> { 1, 2, 3, 4, 5, 6, 7 },
                MinPrice = 5.99m,
                MaxPrice = 448.99m
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_ExistingMousepad_ReturnsMousepad()
        {
            // Arrange
            var expected = UnitTestHelper.Mousepads.First(m => m.Id == 14);
            _mockUnitOfWork.Setup(x => x.MousepadRepository.GetByIdAsync(14)).ReturnsAsync(expected);
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(14);

            // Assert
            Assert.AreEqual(expected, actual, "The actual mousepad is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_NotExistingMousepad_ReturnsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.GetByIdAsync(14)).ReturnsAsync((Mousepad)null);
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(14);

            // Assert
            Assert.IsNull(actual, "THe method returned not-null object.");
        }

        [Test]
        public async Task GetManufacturersAsync_NotEmptyDb_ReturnsCollectionOfManufacturers()
        {
            // Arrange
            var mousepads = UnitTestHelper.Mousepads.ToList();
            foreach (var mousepad in mousepads)
                mousepad.Manufacturer = UnitTestHelper.Manufacturers.First(m => m.Id == mousepad.ManufacturerId);

            _mockUnitOfWork.Setup(x => x.MousepadRepository.GetAllAsync()).ReturnsAsync(mousepads);
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);
            var expected = mousepads.Select(m => m.Manufacturer).Distinct();

            // Act
            var actual = await service.GetManufacturersAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetTopMaterialsAsync_NotEmptyDb_ReturnsCollectionOfTopMaterials()
        {
            // Arrange
            var mousepads = UnitTestHelper.Mousepads.ToList();
            foreach (var mousepad in mousepads)
                mousepad.TopMaterial = UnitTestHelper.Materials.First(m => m.Id == mousepad.TopMaterialId);

            _mockUnitOfWork.Setup(x => x.MousepadRepository.GetAllAsync()).ReturnsAsync(mousepads);
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);
            var expected = mousepads.Select(m => m.TopMaterial).Distinct();

            // Act
            var actual = await service.GetTopMaterialsAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetBottomMaterialsAsync_NotEmptyDb_ReturnsCollectionOfBottomMaterials()
        {
            // Arrange
            var mousepads = UnitTestHelper.Mousepads.ToList();
            foreach (var mousepad in mousepads)
                mousepad.BottomMaterial = UnitTestHelper.Materials.First(m => m.Id == mousepad.BottomMaterialId);

            _mockUnitOfWork.Setup(x => x.MousepadRepository.GetAllAsync()).ReturnsAsync(mousepads);
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);
            var expected = mousepads.Select(m => m.BottomMaterial).Distinct();

            // Act
            var actual = await service.GetBottomMaterialsAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetBacklightsAsync_NotEmptyDb_ReturnsCollectionOfBacklights()
        {
            // Arrange
            var mousepads = UnitTestHelper.Mousepads.ToList();
            foreach (var mousepad in mousepads)
                mousepad.Backlight = UnitTestHelper.Backlights.First(b => b.Id == mousepad.BacklightId);

            _mockUnitOfWork.Setup(x => x.MousepadRepository.GetAllAsync()).ReturnsAsync(mousepads);
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);
            var expected = mousepads.Select(m => m.Backlight).Distinct();

            // Act
            var actual = await service.GetBacklightsAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }
    }
}