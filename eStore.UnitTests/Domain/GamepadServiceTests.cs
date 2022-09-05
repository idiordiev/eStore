﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.DomainServices;
using eStore.ApplicationCore.Services;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Domain
{
    [TestFixture]
    public class GamepadServiceTests
    {
        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        private Mock<IUnitOfWork> _mockUnitOfWork;

        [Test]
        public async Task GetPresentAsync_NotEmptyDb_ReturnsCollectionOfGamepads()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted);

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
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted);
            var filterModel = new GamepadFilterModel();

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleCompatibleDevice_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g =>
                !g.IsDeleted && g.CompatibleDevices.Select(d => d.CompatibleDeviceId).Contains(1));
            var filterModel = new GamepadFilterModel
            {
                CompatibleDevicesIds = new List<int> { 1 }
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
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g =>
                !g.IsDeleted && (g.CompatibleDevices.Select(d => d.CompatibleDeviceId).Contains(1) ||
                                 g.CompatibleDevices.Select(d => d.CompatibleDeviceId).Contains(2)));
            var filterModel = new GamepadFilterModel
            {
                CompatibleDevicesIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleConnectionType_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted && g.ConnectionTypeId == 1);
            var filterModel = new GamepadFilterModel
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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g =>
                !g.IsDeleted && (g.ConnectionTypeId == 1 || g.ConnectionTypeId == 2));
            var filterModel = new GamepadFilterModel
            {
                ConnectionTypeIds = new List<int> { 1, 2 }
            };

            // Act
            var actual = await service.GetPresentByFilterAsync(filterModel);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetPresentByFilterAsync_NotDeletedAndSingleFeedback_ReturnsCollection()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted && g.FeedbackId == 1);
            var filterModel = new GamepadFilterModel
            {
                FeedbackIds = new List<int> { 1 }
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
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted && (g.FeedbackId == 1 || g.FeedbackId == 2));
            var filterModel = new GamepadFilterModel
            {
                FeedbackIds = new List<int> { 1, 2 }
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
            _mockUnitOfWork.Setup(x => x.GamepadRepository.Query(It.IsAny<Expression<Func<Gamepad, bool>>>()))
                .Returns((Expression<Func<Gamepad, bool>> predicate) =>
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted && g.ManufacturerId == 3);
            var filterModel = new GamepadFilterModel
            {
                ManufacturerIds = new List<int> { 3 }
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
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected =
                UnitTestHelper.Gamepads.Where(g => !g.IsDeleted && (g.ManufacturerId == 3 || g.ManufacturerId == 4));
            var filterModel = new GamepadFilterModel
            {
                ManufacturerIds = new List<int> { 3, 4 }
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
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted && g.Price >= 34.99m);
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
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted && g.Price <= 34.99m);
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
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted && g.Price >= 34.99m && g.Price <= 44.99m);
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
                    UnitTestHelper.Gamepads.Where(predicate.Compile()));
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);

            var expected = UnitTestHelper.Gamepads.Where(g => !g.IsDeleted);
            var filterModel = new GamepadFilterModel
            {
                ConnectionTypeIds = new List<int> { 1, 2, 3, 4 },
                CompatibleDevicesIds = new List<int> { 1, 2, 3, 4 },
                FeedbackIds = new List<int> { 1, 2, 3 },
                ManufacturerIds = new List<int> { 1, 2, 3, 4, 5, 6, 7 },
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
            var expected = UnitTestHelper.Gamepads.First(g => g.Id == 1);
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
            var gamepads = UnitTestHelper.Gamepads.ToList();
            foreach (var gamepad in gamepads)
                gamepad.Manufacturer = UnitTestHelper.Manufacturers.First(m => m.Id == gamepad.ManufacturerId);

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
            var gamepads = UnitTestHelper.Gamepads.ToList();
            foreach (var gamepad in gamepads)
                gamepad.Feedback = UnitTestHelper.Feedbacks.First(f => f.Id == gamepad.FeedbackId);

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
            var gamepads = UnitTestHelper.Gamepads.ToList();
            foreach (var gamepad in gamepads)
                gamepad.ConnectionType = UnitTestHelper.ConnectionTypes.First(t => t.Id == gamepad.ConnectionTypeId);

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
            var gamepads = UnitTestHelper.Gamepads.ToList();
            foreach (var gamepadCompatibleDevice in gamepads.SelectMany(gamepad => gamepad.CompatibleDevices))
                gamepadCompatibleDevice.CompatibleDevice =
                    UnitTestHelper.CompatibleDevices.First(d => d.Id == gamepadCompatibleDevice.CompatibleDeviceId);
            _mockUnitOfWork.Setup(x => x.GamepadRepository.GetAllAsync()).ReturnsAsync(gamepads);
            IGamepadService service = new GamepadService(_mockUnitOfWork.Object);
            var expected = gamepads.SelectMany(g => g.CompatibleDevices).Select(d => d.CompatibleDevice).Distinct();

            // Act
            var actual = await service.GetCompatibleDevicesAsync();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual, "The actual collection is not equivalent to expected.");
        }
    }
}