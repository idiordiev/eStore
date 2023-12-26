using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Persistence.Repositories;
using eStore.Tests.Common;
using eStore.Tests.Common.EqualityComparers;
using NUnit.Framework;

namespace eStore.Infrastructure.Tests.Persistence;

[TestFixture]
public class GamepadRepositoryTests
{
    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _context = _helper.GetApplicationContext();
        _repository = new GamepadRepository(_context);
    }

    private UnitTestHelper _helper;
    private ApplicationContext _context;
    private IRepository<Gamepad> _repository;

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task GetByIdAsync_ExistingGamepad_ReturnsGamepad(int id)
    {
        // Arrange
        Gamepad expected = _helper.Gamepads.FirstOrDefault(c => c.Id == id);

        // Act
        Gamepad actual = await _repository.GetByIdAsync(id);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual gamepad is not equal to the expected.");
    }

    [TestCase(12)]
    [TestCase(-1)]
    public async Task GetByIdAsync_NotExistingGamepad_ReturnsNull(int id)
    {
        // Arrange

        // Act
        Gamepad actual = await _repository.GetByIdAsync(id);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null gamepad.");
    }

    [Test]
    public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfGamepads()
    {
        // Arrange
        var expected = _helper.Gamepads;

        // Act
        var actual = await _repository.GetAllAsync();

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection of gamepads is not equal to the expected.");
    }

    [Test]
    public async Task Query_WithPredicate_ReturnsSuitableGamepads()
    {
        // Arrange
        var expected = _helper.Gamepads.Where(c => c.Id == 2);

        // Act
        var actual = await _repository.QueryAsync(c => c.Id == 2);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new GamepadEqualityComparer()),
            "The actual collection of gamepads is not equal to the expected.");
    }

    [Test]
    public async Task AddAsync_NewGamepad_AddsGamepadAndSavesToDb()
    {
        // Arrange
        var newGamepad = new Gamepad
        {
            IsDeleted = false, Name = "NewGamepad", Created = new DateTime(2022, 01, 26, 14, 36, 20),
            Description = "Description", Manufacturer = "Manufacturer3", Price = 44.99m,
            LastModified = new DateTime(2022, 01, 26, 14, 36, 20),
            ConnectionType = "ConnectionType1", Weight = 260, Feedback = "Feedback1", BigImageUrl = "big16.png",
            ThumbnailImageUrl = "thumbnail16.png"
        };

        // Act
        await _repository.AddAsync(newGamepad);

        // Assert
        Assert.That(_context.Gamepads.Count(), Is.EqualTo(5), "The new gamepad has not been added to the context.");
        Assert.That(await _context.Gamepads.FindAsync(16), Is.Not.Null,
            "The new gamepad has been added with the wrong ID.");
    }

    [Test]
    public async Task UpdateAsync_ExistingGamepad_UpdatesGamepadAndSavesToDb()
    {
        // Arrange
        Gamepad gamepad = await _context.Gamepads.FindAsync(1);

        // Act
        gamepad.Name = "Name1";
        await _repository.UpdateAsync(gamepad);

        // Assert
        Assert.That((await _context.Gamepads.FindAsync(gamepad.Id)).Name, Is.EqualTo("Name1"),
            "The gamepad has not been updated.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task DeleteAsync_ExistingGamepad_DeletesGamepadAndSavesToDb(int id)
    {
        // Arrange

        // Act
        await _repository.DeleteAsync(id);

        // Assert
        Assert.That(_context.Gamepads.Count(), Is.EqualTo(3), "Any gamepads has not been deleted.");
        Assert.That(await _context.Gamepads.FindAsync(id), Is.Null, "The selected gamepad has not been deleted.");
    }

    [TestCase(12)]
    [TestCase(-1)]
    public Task DeleteAsync_NotExistingGamepad_ThrowsArgumentNullException(int id)
    {
        // Arrange

        // Act
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

        // Assert
        Assert.That(exception, Is.Not.Null, "The method has not thrown the ArgumentNullException.");
        return Task.CompletedTask;
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}