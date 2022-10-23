using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Persistence.Repositories;
using eStore.Tests.Common;
using Moq;
using NUnit.Framework;

namespace eStore.Infrastructure.Tests.Persistence
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [SetUp]
        public Task Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _unitOfWork = new UnitOfWork(_context);
            return Task.CompletedTask;
        }

        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;

        [Test]
        public Task CustomerRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            var instance = _unitOfWork.CustomerRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<CustomerRepository>(), "UnitOfWork returned wrong implementation.");
            return Task.CompletedTask;
        }

        [Test]
        public Task CustomerRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            var first = _unitOfWork.CustomerRepository;
            var second = _unitOfWork.CustomerRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
            return Task.CompletedTask;
        }

        [Test]
        public Task GamepadRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            var instance = _unitOfWork.GamepadRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<GamepadRepository>(), "UnitOfWork returned wrong implementation.");
            return Task.CompletedTask;
        }

        [Test]
        public Task GamepadRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            var first = _unitOfWork.GamepadRepository;
            var second = _unitOfWork.GamepadRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
            return Task.CompletedTask;
        }

        [Test]
        public Task GoodsRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            var instance = _unitOfWork.GoodsRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<GoodsRepository>(), "UnitOfWork returned wrong implementation.");
            return Task.CompletedTask;
        }

        [Test]
        public Task GoodsRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            var first = _unitOfWork.GoodsRepository;
            var second = _unitOfWork.GoodsRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
            return Task.CompletedTask;
        }

        [Test]
        public Task KeyboardRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            var instance = _unitOfWork.KeyboardRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<KeyboardRepository>(), "UnitOfWork returned wrong implementation.");
            return Task.CompletedTask;
        }

        [Test]
        public Task KeyboardRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            var first = _unitOfWork.KeyboardRepository;
            var second = _unitOfWork.KeyboardRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
            return Task.CompletedTask;
        }

        [Test]
        public Task MousepadRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            var instance = _unitOfWork.MousepadRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<MousepadRepository>(), "UnitOfWork returned wrong implementation.");
            return Task.CompletedTask;
        }

        [Test]
        public Task MousepadRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            var first = _unitOfWork.MousepadRepository;
            var second = _unitOfWork.MousepadRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
            return Task.CompletedTask;
        }

        [Test]
        public Task MouseRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            var instance = _unitOfWork.MouseRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<MouseRepository>(), "UnitOfWork returned wrong implementation.");
            return Task.CompletedTask;
        }

        [Test]
        public Task MouseRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            var first = _unitOfWork.MouseRepository;
            var second = _unitOfWork.MouseRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
            return Task.CompletedTask;
        }

        [Test]
        public Task OrderItemRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            var instance = _unitOfWork.OrderItemRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<OrderItemRepository>(), "UnitOfWork returned wrong implementation.");
            return Task.CompletedTask;
        }

        [Test]
        public Task OrderItemRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            var first = _unitOfWork.OrderItemRepository;
            var second = _unitOfWork.OrderItemRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
            return Task.CompletedTask;
        }

        [Test]
        public Task OrderRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            var instance = _unitOfWork.OrderRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<OrderRepository>(), "UnitOfWork returned wrong implementation.");
            return Task.CompletedTask;
        }

        [Test]
        public Task OrderRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            var first = _unitOfWork.OrderRepository;
            var second = _unitOfWork.OrderRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
            return Task.CompletedTask;
        }

        [Test]
        public Task Dispose_FirstCall_DisposesContext()
        {
            // Arrange
            var context = new Mock<ApplicationContext>();
            context.Setup(x => x.Dispose());
            IUnitOfWork unitOfWork = new UnitOfWork(context.Object);

            // Act
            unitOfWork.Dispose();

            // Assert
            context.Verify(x => x.Dispose(), Times.Once);
            return Task.CompletedTask;
        }

        [Test]
        public Task Dispose_SecondCall_DisposeMethodCalledOnce()
        {
            // Arrange
            var context = new Mock<ApplicationContext>();
            context.Setup(x => x.Dispose());
            IUnitOfWork unitOfWork = new UnitOfWork(context.Object);

            // Act
            unitOfWork.Dispose();
            unitOfWork.Dispose();

            // Assert
            context.Verify(x => x.Dispose(), Times.Once);
            return Task.CompletedTask;
        }
    }
}