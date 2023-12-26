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
public class KeyboardRepositoryTests
{
    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _context = _helper.GetApplicationContext();
        _repository = new KeyboardRepository(_context);
    }

    private UnitTestHelper _helper;
    private ApplicationContext _context;
    private IRepository<Keyboard> _repository;

    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    public async Task GetByIdAsync_ExistingKeyboard_ReturnsKeyboard(int id)
    {
        // Arrange
        Keyboard expected = _helper.Keyboards.FirstOrDefault(c => c.Id == id);

        // Act
        Keyboard actual = await _repository.GetByIdAsync(id);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual keyboard is not equal to the expected.");
    }

    [TestCase(12)]
    [TestCase(1)]
    [TestCase(-1)]
    public async Task GetByIdAsync_NotExistingKeyboard_ReturnsNull(int id)
    {
        // Arrange

        // Act
        Keyboard actual = await _repository.GetByIdAsync(id);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null keyboard.");
    }

    [Test]
    public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfKeyboards()
    {
        // Arrange
        var expected = _helper.Keyboards;

        // Act
        var actual = await _repository.GetAllAsync();

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection of keyboards is not equal to the expected.");
    }

    [Test]
    public async Task Query_WithPredicate_ReturnsSuitableKeyboards()
    {
        // Arrange
        var expected = _helper.Keyboards.Where(c => c.Id == 5);

        // Act
        var actual = await _repository.QueryAsync(c => c.Id == 5);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new KeyboardEqualityComparer()),
            "The actual collection of keyboards is not equal to the expected.");
    }

    [Test]
    public async Task AddAsync_NewKeyboard_AddsKeyboardAndSavesToDb()
    {
        // Arrange
        var newKeyboard = new Keyboard
        {
            IsDeleted = false, Name = "NewKeyboard", Created = new DateTime(2022, 01, 27, 14, 56, 20),
            LastModified = new DateTime(2022, 01, 27, 14, 56, 20),
            Description = "Description", Manufacturer = "Manufacturer5", Price = 67.99m, BigImageUrl = "big16.png",
            ThumbnailImageUrl = "thumbnail16.png",
            Length = 450, Width = 140, Height = 35, Weight = 800, Backlight = "Backlight2", Size = "Size2",
            Type = "Type2",
            ConnectionType = "ConnectionType2", SwitchId = 1, FrameMaterial = "Material3",
            KeycapMaterial = "Material2",
            KeyRollover = "Rollover2"
        };

        // Act
        await _repository.AddAsync(newKeyboard);

        // Assert
        Assert.That(_context.Keyboards.Count(), Is.EqualTo(6),
            "The new keyboard has not been added to the context.");
        Assert.That(await _context.Keyboards.FindAsync(16), Is.Not.Null,
            "The new keyboard has been added with the wrong ID.");
    }

    [Test]
    public async Task UpdateAsync_ExistingKeyboard_UpdatesKeyboardAndSavesToDb()
    {
        // Arrange
        Keyboard keyboard = await _context.Keyboards.FindAsync(5);

        // Act
        keyboard.Name = "NewName";
        await _repository.UpdateAsync(keyboard);

        // Assert
        Assert.That((await _context.Keyboards.FindAsync(keyboard.Id)).Name, Is.EqualTo("NewName"),
            "The keyboard has not been updated.");
    }

    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    public async Task DeleteAsync_ExistingKeyboard_DeletesKeyboardAndSavesToDb(int id)
    {
        // Arrange

        // Act
        await _repository.DeleteAsync(id);

        // Assert
        Assert.That(_context.Keyboards.Count(), Is.EqualTo(4), "Any keyboards has not been deleted.");
        Assert.That(await _context.Keyboards.FindAsync(id), Is.Null, "The selected keyboard has not been deleted.");
    }

    [TestCase(12)]
    [TestCase(1)]
    [TestCase(-1)]
    public Task DeleteAsync_NotExistingKeyboard_ThrowsArgumentNullException(int id)
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