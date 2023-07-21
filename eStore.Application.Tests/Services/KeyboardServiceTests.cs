using System;
using System.Collections.Generic;
using System.Linq;
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
public class KeyboardServiceTests
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
    public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfKeyboards()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted);

        // Act
        var actual = await service.GetPresentAsync();

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_OnlyNotDeleted_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted);
        var filterModel = new KeyboardFilterModel();

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleConnectionType_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.ConnectionType == "ConnectionType1");
        var filterModel = new KeyboardFilterModel
        {
            ConnectionTypes = new List<string> { "ConnectionType1" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleConnectionTypes_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k =>
            !k.IsDeleted &&
            (k.ConnectionType.Equals("ConnectionType1", StringComparison.InvariantCultureIgnoreCase) ||
             k.ConnectionType.Equals("ConnectionType2", StringComparison.InvariantCultureIgnoreCase)));
        var filterModel = new KeyboardFilterModel
        {
            ConnectionTypes = new List<string> { "ConnectionType1", "ConnectionType2" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleSwitch_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.SwitchId == 1);
        var filterModel = new KeyboardFilterModel
        {
            SwitchIds = new List<int?> { 1 }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleSwitches_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && (k.SwitchId == 1 || k.SwitchId == 2));
        var filterModel = new KeyboardFilterModel
        {
            SwitchIds = new List<int?> { 1, 2 }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleType_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Type == "Type1");
        var filterModel = new KeyboardFilterModel
        {
            KeyboardTypes = new List<string> { "Type1" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleTypes_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && (k.Type == "Type1" || k.Type == "Type2"));
        var filterModel = new KeyboardFilterModel
        {
            KeyboardTypes = new List<string> { "Type1", "Type2" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleSize_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Size == "Size1");
        var filterModel = new KeyboardFilterModel
        {
            KeyboardSizes = new List<string> { "Size1" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleSizes_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && (k.Size == "Size1" || k.Size == "Size2"));
        var filterModel = new KeyboardFilterModel
        {
            KeyboardSizes = new List<string> { "Size1", "Size2" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleBacklight_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Backlight == "Backlight1");
        var filterModel = new KeyboardFilterModel
        {
            Backlights = new List<string> { "Backlight1" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleBacklights_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected =
            _helper.Keyboards.Where(k =>
                !k.IsDeleted && (k.Backlight == "Backlight1" || k.Backlight == "Backlight2"));
        var filterModel = new KeyboardFilterModel
        {
            Backlights = new List<string> { "Backlight1", "Backlight2" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleKeyRollover_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.KeyRollover == "Rollover1");
        var filterModel = new KeyboardFilterModel
        {
            KeyRollovers = new List<string> { "Rollover1" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleKeyRollovers_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected =
            _helper.Keyboards.Where(k =>
                !k.IsDeleted && (k.KeyRollover == "Rollover1" || k.KeyRollover == "Rollover2"));
        var filterModel = new KeyboardFilterModel
        {
            KeyRollovers = new List<string> { "Rollover1", "Rollover2" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndSingleManufacturer_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Manufacturer == "Manufacturer4");
        var filterModel = new KeyboardFilterModel
        {
            Manufacturers = new List<string> { "Manufacturer4" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMultipleManufacturers_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected =
            _helper.Keyboards.Where(k =>
                !k.IsDeleted && (k.Manufacturer == "Manufacturer4" || k.Manufacturer == "Manufacturer5"));
        var filterModel = new KeyboardFilterModel
        {
            Manufacturers = new List<string> { "Manufacturer4", "Manufacturer5" }
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMinPrice_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Price >= 47.99m);
        var filterModel = new KeyboardFilterModel
        {
            MinPrice = 47.99m
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndMaxPrice_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted && k.Price <= 47.99m);
        var filterModel = new KeyboardFilterModel
        {
            MaxPrice = 47.99m
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndPrice_ReturnsCollection()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
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
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetPresentByFilterAsync_NotDeletedAndAllOtherParameters_ReturnsFirst()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.Query(It.IsAny<Func<Keyboard, bool>>()))
            .Returns((Func<Keyboard, bool> predicate) => _helper.Keyboards.Where(predicate));
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        var expected = _helper.Keyboards.Where(k => !k.IsDeleted);
        var filterModel = new KeyboardFilterModel
        {
            KeyboardTypes = new List<string> { "Type1", "Type2", "Type3" },
            KeyboardSizes = new List<string> { "Size1", "Size2", "Size3" },
            ConnectionTypes = new List<string>
            {
                "ConnectionType1", "ConnectionType2", "ConnectionType3",
                "ConnectionType4"
            },
            SwitchIds = new List<int?> { 1, 2, 3, 4, 5, null },
            Backlights = new List<string> { "Backlight1", "Backlight2", "Backlight3", "Backlight4" },
            KeyRollovers = new List<string> { "Rollover1", "Rollover2", "Rollover3" },
            Manufacturers = new List<string>
            {
                "Manufacturer1", "Manufacturer2", "Manufacturer3", "Manufacturer4",
                "Manufacturer5", "Manufacturer6", "Manufacturer7"
            },
            MinPrice = 17.99m,
            MaxPrice = 97.99m
        };

        // Act
        var actual = await service.GetPresentByFilterAsync(filterModel);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetByIdAsync_ExistingKeyboard_ReturnsKeyboard()
    {
        // Arrange   
        Keyboard expected = _helper.Keyboards.First(k => k.Id == 5);
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetByIdAsync(5)).ReturnsAsync(expected);
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        // Act
        Keyboard actual = await service.GetByIdAsync(5);

        // Assert
        Assert.That(actual, Is.EqualTo(expected), "The actual keyboard is not equal to expected.");
    }

    [Test]
    public async Task GetByIdAsync_NotExistingKeyboard_ReturnsNull()
    {
        // Arrange
        _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetByIdAsync(1)).ReturnsAsync((Keyboard)null);
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);

        // Act
        Keyboard actual = await service.GetByIdAsync(1);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null object.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetSwitchesAsync_NotEmptyDb_ReturnsCollectionOfSwitches()
    {
        // Arrange
        var keyboards = _helper.Keyboards.ToList();
        foreach (Keyboard keyboard in keyboards)
        {
            keyboard.Switch = _helper.KeyboardSwitches.FirstOrDefault(sw => sw.Id == keyboard.SwitchId);
        }

        _mockUnitOfWork.Setup(x => x.KeyboardRepository.GetAllAsync()).ReturnsAsync(keyboards);
        IKeyboardService service = new KeyboardService(_mockUnitOfWork.Object);
        var expected = keyboards.Select(k => k.Switch).Where(s => s != null).Distinct();

        // Act
        var actual = await service.GetSwitchesAsync();

        // Assert
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equal to expected.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equal to expected.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equal to expected.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equal to expected.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equal to expected.");
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
        Assert.That(actual, Is.EqualTo(expected), "The actual collection is not equal to expected.");
    }
}