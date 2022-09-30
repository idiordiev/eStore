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
    public class KeyboardServiceTests
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
        public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfKeyboards()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted);

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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted);
            var filterModel = new KeyboardFilterModel();

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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.ConnectionType.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new KeyboardFilterModel
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k =>
                !k.IsDeleted && (k.ConnectionType.Equals("ConnectionType1", StringComparison.InvariantCultureIgnoreCase) || k.ConnectionType.Equals("ConnectionType2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new KeyboardFilterModel
            {
                ConnectionTypes = new List<string> { "ConnectionType1", "ConnectionType2" }
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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.SwitchId == 1);
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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && (k.SwitchId == 1 || k.SwitchId == 2));
            var filterModel = new KeyboardFilterModel
            {
                SwitchIds = new List<int?> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Type1")]
        [TestCase("TypE1")]
        [TestCase("type1")]
        [TestCase("Type1111")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleType_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Type.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new KeyboardFilterModel
            {
                KeyboardTypes = new List<string> { paramValue }
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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && (k.Type.Equals("Type1", StringComparison.InvariantCultureIgnoreCase) || k.Type.Equals("Type2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new KeyboardFilterModel
            {
                KeyboardTypes = new List<string> { "Type1", "Type2" }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Size1")]
        [TestCase("SizE1")]
        [TestCase("size1")]
        [TestCase("Size1111")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleSize_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Size.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new KeyboardFilterModel
            {
                KeyboardSizes = new List<string> { paramValue }
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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && (k.Size.Equals("Size1", StringComparison.InvariantCultureIgnoreCase) || k.Size.Equals("Size2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new KeyboardFilterModel
            {
                KeyboardSizes = new List<string> { "Size1", "Size2" }
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Backlight.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new KeyboardFilterModel
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected =
                _helper.Keyboards.Where(k => !k.IsDeleted && (k.Backlight.Equals("Backlight1", StringComparison.InvariantCultureIgnoreCase) || k.Backlight.Equals("Backlight2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new KeyboardFilterModel
            {
                Backlights = new List<string> { "Backlight1", "Backlight2" }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Rollover1")]
        [TestCase("RollOver1")]
        [TestCase("RoLlOvEr1")]
        [TestCase("rollover1")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleKeyRollover_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.KeyRollover.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new KeyboardFilterModel
            {
                KeyRollovers = new List<string> { paramValue }
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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected =
                _helper.Keyboards.Where(k => !k.IsDeleted && (k.KeyRollover.Equals("Rollover1", StringComparison.InvariantCultureIgnoreCase) || k.KeyRollover.Equals("Rollover2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new KeyboardFilterModel
            {
                KeyRollovers = new List<string> { "Rollover1", "Rollover2" }
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Manufacturer.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new KeyboardFilterModel
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected =
                _helper.Keyboards.Where(k => !k.IsDeleted && (k.Manufacturer.Equals("Manufacturer4", StringComparison.InvariantCultureIgnoreCase) || k.Manufacturer.Equals("Manufacturer5", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new KeyboardFilterModel
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
            _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Expression<Func<Keyboard, bool>>>()))
                .Returns((Expression<Func<Keyboard, bool>> predicate) =>
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Price >= 47.99m);
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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Price <= 47.99m);
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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Price <= 57.99m && k.Price >= 47.99m);
            var filterModel = new KeyboardFilterModel
            {
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
                    _helper.Keyboards.Where(predicate.Compile()));
            IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

            var expected = _helper.Keyboards.Where(k => !k.IsDeleted);
            var filterModel = new KeyboardFilterModel
            {
                KeyboardTypes = new List<string> { "Type1", "Type2", "Type3" },
                KeyboardSizes = new List<string> { "Size1", "Size2", "Size3" },
                ConnectionTypes = new List<string> { "ConnectionType1", "ConnectionType2", "ConnectionType3", 
                    "ConnectionType4" },
                SwitchIds = new List<int?> { 1, 2, 3, 4, 5, null },
                Backlights = new List<string> { "Backlight1", "Backlight2", "Backlight3", "Backlight4" },
                KeyRollovers = new List<string> { "Rollover1", "Rollover2", "Rollover3" },
                Manufacturers = new List<string> { "Manufacturer1", "Manufacturer2", "Manufacturer3", "Manufacturer4", 
                    "Manufacturer5", "Manufacturer6", "Manufacturer7" },
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
            var expected = _helper.Keyboards.First(k => k.Id == 5);
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
            var keyboards = _helper.Keyboards.ToList();
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
            var keyboards = _helper.Keyboards.ToList();
            foreach (var keyboard in keyboards)
                keyboard.Switch = _helper.KeyboardSwitches.FirstOrDefault(sw => sw.Id == keyboard.SwitchId);

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
            var keyboards = _helper.Keyboards.ToList();
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
            var keyboards = _helper.Keyboards.ToList();
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
            var keyboards = _helper.Keyboards.ToList();
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
            var keyboards = _helper.Keyboards.ToList();
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
            var keyboards = _helper.Keyboards.ToList();
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