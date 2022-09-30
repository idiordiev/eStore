﻿using System;
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
    public class MousepadServiceTests
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
        public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfMousepads()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted);

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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted);
            var filterModel = new MousepadFilterModel();

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Material4")]
        [TestCase("MaterIal4")]
        [TestCase("material4")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleBottomMaterial_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && m.BottomMaterial.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new MousepadFilterModel
            {
                BottomMaterials = new List<string> { paramValue }
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m =>
                !m.IsDeleted && (m.BottomMaterial.Equals("Material1", StringComparison.InvariantCultureIgnoreCase) || m.BottomMaterial.Equals("Material2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new MousepadFilterModel
            {
                BottomMaterials = new List<string> { "Material1", "Material2" }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Material5")]
        [TestCase("MaterIal5")]
        [TestCase("material5")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleTopMaterial_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && m.TopMaterial.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new MousepadFilterModel
            {
                TopMaterials = new List<string> { paramValue }
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected =
                _helper.Mousepads.Where(m => !m.IsDeleted && (m.TopMaterial.Equals("Material1", StringComparison.InvariantCultureIgnoreCase) || m.TopMaterial.Equals("Material2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new MousepadFilterModel
            {
                TopMaterials = new List<string> { "Material1", "Material2" }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Backlight1")]
        [TestCase("BAcklighT1")]
        [TestCase("BackLiGhT1")]
        [TestCase("backlight1")]
        [TestCase("Backlight1111")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleBacklight_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && m.Backlight.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new MousepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected =
                _helper.Mousepads.Where(m => !m.IsDeleted && (m.Backlight.Equals("Backlight1", StringComparison.InvariantCultureIgnoreCase) || m.Backlight.Equals("Backlight2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new MousepadFilterModel
            {
                Backlights = new List<string> { "Backlight1", "Backlight1" }
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && !m.IsStitched);
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && m.IsStitched);
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && (m.IsStitched || !m.IsStitched));
            var filterModel = new MousepadFilterModel
            {
                IsStitchedValues = new List<bool> { true, false }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Manufacturer4")]
        [TestCase("ManUfacturer4")]
        [TestCase("mAnUfAcTuReR4")]
        [TestCase("manufacturer4")]
        [TestCase("Manufacturer444")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleManufacturer_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && m.Manufacturer.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new MousepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.MousepadRepository.Query(It.IsAny<Expression<Func<Mousepad, bool>>>()))
                .Returns((Expression<Func<Mousepad, bool>> predicate) =>
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && (m.Manufacturer.Equals("Manufacturer4", StringComparison.InvariantCultureIgnoreCase) || m.Manufacturer.Equals("Manufacturer5", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new MousepadFilterModel
            {
                Manufacturers = new List<string> { "Manufacturer4", "Manufacturer5" }
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && m.Price >= 45.99m);
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && m.Price <= 45.99m);
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted && m.Price >= 45.99m && m.Price <= 48.99m);
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
                    _helper.Mousepads.Where(predicate.Compile()));
            IMousepadService service = new MousepadService(_mockUnitOfWork.Object);

            var expected = _helper.Mousepads.Where(m => !m.IsDeleted);
            var filterModel = new MousepadFilterModel
            {
                IsStitchedValues = new List<bool> { true, false },
                BottomMaterials = new List<string> { "Material1", "Material2", "Material3", "Material4", "Material5" },
                TopMaterials = new List<string> { "Material1", "Material2", "Material3", "Material4", "Material5" },
                Manufacturers = new List<string> { "Manufacturer1", "Manufacturer2", "Manufacturer3", "Manufacturer4", 
                    "Manufacturer5", "Manufacturer6", "Manufacturer7" },
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
            var expected = _helper.Mousepads.First(m => m.Id == 14);
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
            var mousepads = _helper.Mousepads.ToList();
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
            var mousepads = _helper.Mousepads.ToList();
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
            var mousepads = _helper.Mousepads.ToList();
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
            var mousepads = _helper.Mousepads.ToList();
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