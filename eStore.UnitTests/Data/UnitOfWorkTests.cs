using System.Threading.Tasks;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.Infrastructure.Data;
using eStore.Infrastructure.Data.Repositories;
using eStore.Infrastructure.Data.UnitOfWork;
using Moq;
using NUnit.Framework;

namespace eStore.UnitTests.Data
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public async Task CustomerRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var instance = unitOfWork.CustomerRepository;

            // Assert
            Assert.IsInstanceOf<CustomerRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public async Task CustomerRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var first = unitOfWork.CustomerRepository;
            var second = unitOfWork.CustomerRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public async Task GamepadRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var instance = unitOfWork.GamepadRepository;

            // Assert
            Assert.IsInstanceOf<GamepadRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public async Task GamepadRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var first = unitOfWork.GamepadRepository;
            var second = unitOfWork.GamepadRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public async Task GoodsRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var instance = unitOfWork.GoodsRepository;

            // Assert
            Assert.IsInstanceOf<GoodsRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public async Task GoodsRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var first = unitOfWork.GoodsRepository;
            var second = unitOfWork.GoodsRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public async Task KeyboardRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var instance = unitOfWork.KeyboardRepository;

            // Assert
            Assert.IsInstanceOf<KeyboardRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public async Task KeyboardRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var first = unitOfWork.KeyboardRepository;
            var second = unitOfWork.KeyboardRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public async Task MousepadRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var instance = unitOfWork.MousepadRepository;

            // Assert
            Assert.IsInstanceOf<MousepadRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public async Task MousepadRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var first = unitOfWork.MousepadRepository;
            var second = unitOfWork.MousepadRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public async Task MouseRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var instance = unitOfWork.MouseRepository;

            // Assert
            Assert.IsInstanceOf<MouseRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public async Task MouseRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var first = unitOfWork.MouseRepository;
            var second = unitOfWork.MouseRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public async Task OrderItemRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var instance = unitOfWork.OrderItemRepository;

            // Assert
            Assert.IsInstanceOf<OrderItemRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public async Task OrderItemRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var first = unitOfWork.OrderItemRepository;
            var second = unitOfWork.OrderItemRepository;

            // Assert
            Assert.AreSame(first, second, "UnitOfWork returned different instances.");
        }
        
        [Test]
        public async Task OrderRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var instance = unitOfWork.OrderRepository;

            // Assert
            Assert.IsInstanceOf<OrderRepository>(instance, "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public async Task OrderRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            
            // Act
            var first = unitOfWork.OrderRepository;
            var second = unitOfWork.OrderRepository;

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