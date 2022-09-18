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
    public class KeyboardServiceTests
    {
        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        private Mock<IUnitOfWork> _mockUnitOfWork;

        [Test]
        public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfKeyboards()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted);

            // Act
            var actual = await service.GetPresentAsync();

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_OnlyNotDeleted_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted);
            var filterModel = new KeyboardFilterModel();

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleConnectionType_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.ConnectionTypeId == 1);
            var filterModel = new KeyboardFilterModel
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k =>
                !k.IsDeleted && (k.ConnectionTypeId == 1 || k.ConnectionTypeId == 2));
            var filterModel = new KeyboardFilterModel
            {
                ConnectionTypeIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleSwitch_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.SwitchId == 1);
            var filterModel = new KeyboardFilterModel
            {
                SwitchIds = new List<int?> { 1 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleSwitches_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && (k.SwitchId == 1 || k.SwitchId == 2));
            var filterModel = new KeyboardFilterModel
            {
                SwitchIds = new List<int?> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleType_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.TypeId == 1);
            var filterModel = new KeyboardFilterModel
            {
                KeyboardTypeIds = new List<int> { 1 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleTypes_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && (k.TypeId == 1 || k.TypeId == 2));
            var filterModel = new KeyboardFilterModel
            {
                KeyboardTypeIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleSize_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.SizeId == 1);
            var filterModel = new KeyboardFilterModel
            {
                KeyboardSizeIds = new List<int> { 1 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleSizes_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && (k.SizeId == 1 || k.SizeId == 2));
            var filterModel = new KeyboardFilterModel
            {
                KeyboardSizeIds = new List<int> { 1, 2 }
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.BacklightId == 1);
            var filterModel = new KeyboardFilterModel
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && (k.BacklightId == 1 || k.BacklightId == 2));
            var filterModel = new KeyboardFilterModel
            {
                BacklightIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleKeyRollover_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.KeyRolloverId == 1);
            var filterModel = new KeyboardFilterModel
            {
                KeyRolloverIds = new List<int> { 1 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleKeyRollovers_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && (k.KeyRolloverId == 1 || k.KeyRolloverId == 2));
            var filterModel = new KeyboardFilterModel
            {
                KeyRolloverIds = new List<int> { 1, 2 }
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.ManufacturerId == 4);
            var filterModel = new KeyboardFilterModel
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && (k.ManufacturerId == 4 || k.ManufacturerId == 5));
            var filterModel = new KeyboardFilterModel
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.Price >= 47.99m);
            var filterModel = new KeyboardFilterModel
            {
                MinPrice = 47.99m
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.Price <= 47.99m);
            var filterModel = new KeyboardFilterModel
            {
                MaxPrice = 47.99m
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted && k.Price <= 57.99m && k.Price >= 47.99m);
            var filterModel = new KeyboardFilterModel
            {
                ManufacturerIds = new List<int> { 1, 2, 3, 4, 5, 6, 7 },
                MinPrice = 47.99m,
                MaxPrice = 57.99m
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    UnitTestHelper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Keyboards.Where(k => !k.IsDeleted);
            var filterModel = new KeyboardFilterModel
            {
                KeyboardTypeIds = new List<int> { 1, 2, 3 },
                KeyboardSizeIds = new List<int> { 1, 2, 3 },
                ConnectionTypeIds = new List<int> { 1, 2, 3, 4 },
                SwitchIds = new List<int?> { 1, 2, 3, 4, 5, null },
                BacklightIds = new List<int> { 1, 2, 3, 4 },
                KeyRolloverIds = new List<int> { 1, 2, 3 },
                ManufacturerIds = new List<int> { 1, 2, 3, 4, 5, 6, 7 },
                MinPrice = 17.99m,
                MaxPrice = 97.99m
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_ExistingKeyboard_ReturnsKeyboard()
        {
            // Arrange   
            var expected = UnitTestHelper.Keyboards.First(k => k.Id == 5);
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetByIdAsync(5)).ReturnsAsync(expected);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(5);

            // Assert
            Assert.AreEqual(expected, actual, "The actual keyboard is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_NotExistingKeyboard_ReturnsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetByIdAsync(1)).ReturnsAsync((Keyboard)null);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(1);

            // Assert
            Assert.IsNull(actual, "The method returned not-null object.");
        }

        [Test]
        public async Task GetManufacturersAsync_NotEmptyDb_ReturnsCollectionOfManufacturers()
        {
            // Arrange
            var keyboards = UnitTestHelper.Keyboards.ToList();
            foreach (var keyboard in keyboards)
                keyboard.Manufacturer = UnitTestHelper.Manufacturers.First(m => m.Id == keyboard.ManufacturerId);

            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetAllAsync()).ReturnsAsync(keyboards);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);
            var expected = keyboards.Select(k => k.Manufacturer).Distinct();

            // Act
            var actual = await service.GetManufacturersAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetSwitchesAsync_NotEmptyDb_ReturnsCollectionOfSwitches()
        {
            // Arrange
            var keyboards = UnitTestHelper.Keyboards.ToList();
            foreach (var keyboard in keyboards)
                keyboard.Switch = UnitTestHelper.KeyboardSwitches.FirstOrDefault(sw => sw.Id == keyboard.SwitchId);

            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetAllAsync()).ReturnsAsync(keyboards);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);
            var expected = keyboards.Select(k => k.Switch).Where(s => s != null).Distinct();

            // Act
            var actual = await service.GetSwitchesAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetSizesAsync_NotEmptyDb_ReturnsCollectionOfKeyboardSizes()
        {
            // Arrange
            var keyboards = UnitTestHelper.Keyboards.ToList();
            foreach (var keyboard in keyboards)
                keyboard.Size = UnitTestHelper.KeyboardSizes.First(s => s.Id == keyboard.SizeId);

            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetAllAsync()).ReturnsAsync(keyboards);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);
            var expected = keyboards.Select(k => k.Size).Distinct();

            // Act
            var actual = await service.GetSizesAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetTypesAsync_NotEmptyDb_ReturnsCollectionOfKeyboardTypes()
        {
            // Arrange
            var keyboards = UnitTestHelper.Keyboards.ToList();
            foreach (var keyboard in keyboards)
                keyboard.Type = UnitTestHelper.KeyboardTypes.First(t => t.Id == keyboard.TypeId);

            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetAllAsync()).ReturnsAsync(keyboards);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);
            var expected = keyboards.Select(k => k.Type).Distinct();

            // Act
            var actual = await service.GetTypesAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetConnectionTypesAsync_NotEmptyDb_ReturnsCollectionOfConnectionTypes()
        {
            // Arrange
            var keyboards = UnitTestHelper.Keyboards.ToList();
            foreach (var keyboard in keyboards)
                keyboard.ConnectionType = UnitTestHelper.ConnectionTypes.First(t => t.Id == keyboard.ConnectionTypeId);

            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetAllAsync()).ReturnsAsync(keyboards);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);
            var expected = keyboards.Select(k => k.ConnectionType).Distinct();

            // Act
            var actual = await service.GetConnectionTypesAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetBacklightsAsync_NotEmptyDb_ReturnsCollectionOfBacklights()
        {
            // Arrange
            var keyboards = UnitTestHelper.Keyboards.ToList();
            foreach (var keyboard in keyboards)
                keyboard.Backlight = UnitTestHelper.Backlights.First(b => b.Id == keyboard.BacklightId);

            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetAllAsync()).ReturnsAsync(keyboards);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);
            var expected = keyboards.Select(k => k.Backlight).Distinct();

            // Act
            var actual = await service.GetBacklightsAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetKeyRolloverAsync_NotEmptyDb_ReturnsCollectionOfKeyboardTypes()
        {
            // Arrange
            var keyboards = UnitTestHelper.Keyboards.ToList();
            foreach (var keyboard in keyboards)
                keyboard.KeyRollover = UnitTestHelper.KeyRollovers.First(r => r.Id == keyboard.KeyRolloverId);

            _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetAllAsync()).ReturnsAsync(keyboards);
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);
            var expected = keyboards.Select(k => k.KeyRollover).Distinct();

            // Act
            var actual = await service.GetKeyRolloverAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equal to expected.");
        }
    }
}