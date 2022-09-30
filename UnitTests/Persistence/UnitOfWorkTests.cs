using eStore.Application.Interfaces.Data;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Persistence.Repositories;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Persistence
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _unitOfWork = new UnitOfWork(_context);
        }
        
        [Test]
        public void CustomerRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            
            // Act
            var instance = _unitOfWork.CustomerRepository;

            // Assert
            Assert.IsInstanceOf<CustomerRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void CustomerRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            
            // Act
            var first = _unitOfWork.CustomerRepository;
            var second = _unitOfWork.CustomerRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public void GamepadRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            
            // Act
            var instance = _unitOfWork.GamepadRepository;

            // Assert
            Assert.IsInstanceOf<GamepadRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void GamepadRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            
            // Act
            var first = _unitOfWork.GamepadRepository;
            var second = _unitOfWork.GamepadRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public void GoodsRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            
            // Act
            var instance = _unitOfWork.GoodsRepository;

            // Assert
            Assert.IsInstanceOf<GoodsRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void GoodsRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            
            // Act
            var first = _unitOfWork.GoodsRepository;
            var second = _unitOfWork.GoodsRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public void KeyboardRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            
            // Act
            var instance = _unitOfWork.KeyboardRepository;

            // Assert
            Assert.IsInstanceOf<KeyboardRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void KeyboardRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            
            // Act
            var first = _unitOfWork.KeyboardRepository;
            var second = _unitOfWork.KeyboardRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public void MousepadRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            
            // Act
            var instance = _unitOfWork.MousepadRepository;

            // Assert
            Assert.IsInstanceOf<MousepadRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void MousepadRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            
            // Act
            var first = _unitOfWork.MousepadRepository;
            var second = _unitOfWork.MousepadRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public void MouseRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            
            // Act
            var instance = _unitOfWork.MouseRepository;

            // Assert
            Assert.IsInstanceOf<MouseRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void MouseRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            
            // Act
            var first = _unitOfWork.MouseRepository;
            var second = _unitOfWork.MouseRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public void OrderItemRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            
            // Act
            var instance = _unitOfWork.OrderItemRepository;

            // Assert
            Assert.IsInstanceOf<OrderItemRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void OrderItemRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            
            // Act
            var first = _unitOfWork.OrderItemRepository;
            var second = _unitOfWork.OrderItemRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public void OrderRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            
            // Act
            var instance = _unitOfWork.OrderRepository;

            // Assert
            Assert.IsInstanceOf<OrderRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void OrderRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            
            // Act
            var first = _unitOfWork.OrderRepository;
            var second = _unitOfWork.OrderRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }

        [Test]
        public void Dispose_FirstCall_DisposesContext()
        {
            // Arrange
            var context = new Mock<ApplicationContext>();
            context.Setup(x => x.Dispose());
            IUnitOfWork unitOfWork = new UnitOfWork(context.Object);
            
            // Act
            unitOfWork.Dispose();

            // Assert
            context.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void Dispose_SecondCall_DisposeMethodCalledOnce()
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
        }
    }
}