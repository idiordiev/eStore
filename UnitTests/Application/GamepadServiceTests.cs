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
    public class GamepadServiceTests
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
        public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfGamepads()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted);

            // Act
            var actual = await service.GetPresentAsync();

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_OnlyNotDeleted_ReturnsNotDeleted()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted);
            var filterModel = new GamepadFilterModel();

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("CompatibleDevice1")]
        [TestCase("CompatibleDevice2")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleCompatibleDevice_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.CompatibleDevices.Contains(paramValue));
            var filterModel = new GamepadFilterModel
            {
                CompatibleDevices = new List<string> { paramValue }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleCompatibleDevices_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g =>
                !g.IsDeleted && (g.CompatibleDevices.Contains("CompatibleDevice1") ||
                                 g.CompatibleDevices.Contains("CompatibleDevice2")));
            var filterModel = new GamepadFilterModel
            {
                CompatibleDevices = new List<string> { "CompatibleDevice1", "CompatibleDevice2" }
            };

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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.ConnectionType.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new GamepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && (g.ConnectionType.Equals("ConnectionType1", StringComparison.InvariantCultureIgnoreCase) || g.ConnectionType.Equals("ConnectionType2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new GamepadFilterModel
            {
                ConnectionTypes = new List<string> { "ConnectionType1", "ConnectionType2" }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [TestCase("Feedback1")]
        [TestCase("FeedBack1")]
        [TestCase("FEedback1")]
        [TestCase("feedback1")]
        [TestCase("Feedback1111")]
        [TestCase("")]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleFeedback_ReturnsCollection(string paramValue)
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Feedback.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new GamepadFilterModel
            {
                Feedbacks = new List<string> { paramValue }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndMultipleFeedbacks_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && (g.Feedback.Equals("Feedback1", StringComparison.InvariantCultureIgnoreCase) || g.Feedback.Equals("Feedback2", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new GamepadFilterModel
            {
                Feedbacks = new List<string> { "Feedback1", "Feedback2" }
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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Manufacturer.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase));
            var filterModel = new GamepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected =
                _helper.Gamepads.Where(g => !g.IsDeleted && (g.Manufacturer.Equals("Manufacturer3", StringComparison.InvariantCultureIgnoreCase) || g.Manufacturer.Equals("Manufacturer4", StringComparison.InvariantCultureIgnoreCase)));
            var filterModel = new GamepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Price >= 34.99m);
            var filterModel = new GamepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Price <= 34.99m);
            var filterModel = new GamepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Price >= 34.99m && g.Price <= 44.99m);
            var filterModel = new GamepadFilterModel
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
        public async Task GetPresentByFilterAsync_NotDeletedAndAllOtherParameters_ReturnsNotDeleted()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    _helper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = _helper.Gamepads.Where(g => !g.IsDeleted);
            var filterModel = new GamepadFilterModel
            {
                ConnectionTypes = new List<string> { "ConnectionType1", "ConnectionType2", "ConnectionType3", "ConnectionType4" },
                CompatibleDevices = new List<string> { "CompatibleDevice1", "CompatibleDevice2", "CompatibleDevice3", "CompatibleDevice4" },
                Feedbacks = new List<string> { "Feedback1", "Feedback2", "Feedback3" },
                Manufacturers = new List<string> { "Manufacturer1", "Manufacturer2", "Manufacturer3", "Manufacturer4", 
                    "Manufacturer5", "Manufacturer6", "Manufacturer7" },
                MinPrice = 4.99m,
                MaxPrice = 444.99m
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_ExistingGamepad_ReturnsGamepad()
        {
            // Arrange
            var expected = _helper.Gamepads.First(g => g.Id == 1);
            _mockUnitOfWork.Setup(x => x.GamepadRepository.GetByIdAsync(1)).ReturnsAsync(expected);
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(1);

            // Assert
            Assert.AreEqual(expected, actual, "The actual gamepad is not equal to expected.");
        }

        [Test]
        public async Task GetByIdAsync_NotExistingGamepad_ReturnsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.GetByIdAsync(1)).ReturnsAsync((Gamepad)null);
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            // Act
            var actual = await service.GetByIdAsync(1);

            // Assert
            Assert.IsNull(actual, "The method returned not-null object.");
        }

        [Test]
        public async Task GetManufacturersAsync_NotEmptyDb_ReturnsCollectionOfManufacturers()
        {
            // Arrange
            var gamepads = _helper.Gamepads.ToList();
            _mockUnitOfWork.Setup(x => x.GamepadRepository.GetAllAsync()).ReturnsAsync(gamepads);
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);
            var expected = gamepads.Select(g => g.Manufacturer).Distinct();

            // Act
            var actual = await service.GetManufacturersAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equivalent to expected.");
        }

        [Test]
        public async Task GetFeedbacksAsync_NotEmptyDb_ReturnsCollectionOfFeedbacks()
        {
            // Arrange
            var gamepads = _helper.Gamepads.ToList();
            _mockUnitOfWork.Setup(x => x.GamepadRepository.GetAllAsync()).ReturnsAsync(gamepads);
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);
            var expected = gamepads.Select(g => g.Feedback).Distinct();

            // Act
            var actual = await service.GetFeedbacksAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equivalent to expected.");
        }

        [Test]
        public async Task GetConnectionTypesAsync_NotEmptyDb_ReturnsCollectionOfConnectionTypes()
        {
            // Arrange
            var gamepads = _helper.Gamepads.ToList();
            _mockUnitOfWork.Setup(x => x.GamepadRepository.GetAllAsync()).ReturnsAsync(gamepads);
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);
            var expected = gamepads.Select(g => g.ConnectionType).Distinct();

            // Act
            var actual = await service.GetConnectionTypesAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equivalent to expected.");
        }

        [Test]
        public async Task GetCompatibleDevicesAsync_NotEmptyDb_ReturnsCollectionOfCompatibleDevices()
        {
            // Arrange
            var gamepads = _helper.Gamepads.ToList();
            _mockUnitOfWork.Setup(x => x.GamepadRepository.GetAllAsync()).ReturnsAsync(gamepads);
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);
            var expected = gamepads.SelectMany(g => g.CompatibleDevices).Distinct();

            // Act
            var actual = await service.GetCompatibleDevicesAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equivalent to expected.");
        }
    }
}