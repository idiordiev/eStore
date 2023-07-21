using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Application.Services;
using eStore.Domain.Entities;
using eStore.Tests.Common;
using eStore.Tests.Common.EqualityComparers;
using Moq;
using NUnit.Framework;

namespace eStore.Application.Tests.Services;

[TestFixture]
public class GamepadServiceTests
{
    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    private UnitTestHelper _helper;
    private Mock<IUnitOfWork> _mockUnitOfWork;

    [Test]
    public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfGamepads()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g => !g.IsDeleted);

        // Act
        var actual = await service.GetPresentAsync();

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_OnlyNotDeleted_ReturnsNotDeleted()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g => !g.IsDeleted);
        var filterModel = new GamepadFilterModel();

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleCompatibleDevice_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected =
            _helper.Gamepads.Where(g => !g.IsDeleted && g.CompatibleDevices.Contains("CompatibleDevice1"));
        var filterModel = new GamepadFilterModel
        {
            CompatibleDevices = new List<string> { "CompatibleDevice1" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleCompatibleDevices_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
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
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleConnectionType_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.ConnectionType == "ConnectionType1");
        var filterModel = new GamepadFilterModel
        {
            ConnectionTypes = new List<string> { "ConnectionType1" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleConnectionTypes_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g =>
            !g.IsDeleted && (g.ConnectionType == "ConnectionType1" || g.ConnectionType == "ConnectionType2"));
        var filterModel = new GamepadFilterModel
        {
            ConnectionTypes = new List<string> { "ConnectionType1", "ConnectionType2" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleFeedback_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Feedback == "Feedback1");
        var filterModel = new GamepadFilterModel
        {
            Feedbacks = new List<string> { "Feedback1" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleFeedbacks_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g =>
            !g.IsDeleted && (g.Feedback == "Feedback1" || g.Feedback == "Feedback2"));
        var filterModel = new GamepadFilterModel
        {
            Feedbacks = new List<string> { "Feedback1", "Feedback2" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleManufacturer_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Manufacturer.Equals("Manufacturer3"));
        var filterModel = new GamepadFilterModel
        {
            Manufacturers = new List<string> { "Manufacturer3" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleManufacturers_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected =
            _helper.Gamepads.Where(g =>
                !g.IsDeleted && (g.Manufacturer == "Manufacturer3" || g.Manufacturer == "Manufacturer4"));
        var filterModel = new GamepadFilterModel
        {
            Manufacturers = new List<string> { "Manufacturer3", "Manufacturer4" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMinPrice_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Price >= 34.99m);
        var filterModel = new GamepadFilterModel
        {
            MinPrice = 34.99m
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMaxPrice_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g => !g.IsDeleted && g.Price <= 34.99m);
        var filterModel = new GamepadFilterModel
        {
            MaxPrice = 34.99m
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndPrice_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
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
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndAllOtherParameters_ReturnsNotDeleted()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.QueryAsync(It.IsAny<Expression<Func<Gamepad, bool>>>()))
            .ReturnsAsync((Expression<Func<Gamepad, bool>> predicate) => _helper.Gamepads.Where(predicate.Compile()));
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        var expected = _helper.Gamepads.Where(g => !g.IsDeleted);
        var filterModel = new GamepadFilterModel
        {
            ConnectionTypes = new List<string>
                { "ConnectionType1", "ConnectionType2", "ConnectionType3", "ConnectionType4" },
            CompatibleDevices = new List<string>
                { "CompatibleDevice1", "CompatibleDevice2", "CompatibleDevice3", "CompatibleDevice4" },
            Feedbacks = new List<string> { "Feedback1", "Feedback2", "Feedback3" },
            Manufacturers = new List<string>
            {
                "Manufacturer1", "Manufacturer2", "Manufacturer3", "Manufacturer4", "Manufacturer5",
                "Manufacturer6", "Manufacturer7"
            },
            MinPrice = 4.99m,
            MaxPrice = 444.99m
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetByIdAsync_ExistingGamepad_ReturnsGamepad()
    {
        // Arrange
        Gamepad expected = _helper.Gamepads.First(g => g.Id == 1);
        _mockUnitOfWork.Setup(x => x.GamepadRepository.GetByIdAsync(1)).ReturnsAsync(expected);
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        // Act
        Gamepad actual = await service.GetByIdAsync(1);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual gamepad is not equal to expected.");
    }

    [Test]
    public async Task GetByIdAsync_NotExistingGamepad_ReturnsNull()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.GamepadRepository.GetByIdAsync(1)).ReturnsAsync((Gamepad)null);
        IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

        // Act
        Gamepad actual = await service.GetByIdAsync(1);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null object.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equivalent to expected.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equivalent to expected.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equivalent to expected.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equivalent to expected.");
    }
}