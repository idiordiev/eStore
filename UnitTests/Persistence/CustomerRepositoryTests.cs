using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Persistence.Repositories;
using NUnit.Framework;

namespace eStore.UnitTests.Persistence
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetByIdAsync_ExistingCustomer_ReturnsCustomer(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Customer> repo = new CustomerRepository(context);
            var expected = UnitTestHelper.Customers.FirstOrDefault(c => c.Id == id);
            
            // Act
            var actual = await repo.GetByIdAsync(id);
            
            // Assert
            Assert.AreEqual(expected, actual, "The actual customer is not equal to the expected.");
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingCustomer_ReturnsNull(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Customer> repo = new CustomerRepository(context);

            // Act
            var actual = await repo.GetByIdAsync(id);

            // Assert
            Assert.IsNull(actual, "The method returned not-null customer.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfCustomers()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Customer> repo = new CustomerRepository(context);
            var expected = UnitTestHelper.Customers;
            
            // Act
            var actual = await repo.GetAllAsync();

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of customers is not equal to the expected.");
        }

        [Test]
        public async Task Query_WithPredicate_ReturnsSuitableCustomers()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Customer> repo = new CustomerRepository(context);
            var expected = UnitTestHelper.Customers.Where(c => c.Id == 2);

            // Act
            var actual = repo.Query(c => c.Id == 2);

            // Assert
            Assert.AreEqual(expected, actual, "The actual collection of customers is not equal to the expected.");
        }

        [Test]
        public async Task AddAsync_NewCustomer_AddsCustomerAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Customer> repo = new CustomerRepository(context);
            var newCustomer = new Customer()
            {
                IsDeleted = false,
                IdentityId = Guid.NewGuid().ToString(),
                Email = "Email3",
                ShoppingCart = new ShoppingCart()
            };
            
            // Act
            await repo.AddAsync(newCustomer);

            // Assert
            Assert.AreEqual(context.Customers.Count(), 3, "The new customer has not been added to the context.");
            Assert.IsNotNull(await context.Customers.FindAsync(3), "The new customer has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingCustomer_UpdatesCustomerAndSavesToDb()
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Customer> repo = new CustomerRepository(context);
            var customer = await context.Customers.FindAsync(1);
            
            // Act
            customer.FirstName = "Name1";
            await repo.UpdateAsync(customer);

            // Assert
            Assert.AreEqual((await context.Customers.FindAsync(customer.Id)).FirstName, "Name1", "The customer has not been updated.");
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task DeleteAsync_ExistingCustomer_DeletesCustomerAndSavesToDb(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Customer> repo = new CustomerRepository(context);
            
            // Act
            await repo.DeleteAsync(id);

            // Assert
            Assert.AreEqual(context.Customers.Count(), 1, "Any customers has not been deleted.");
            Assert.IsNull(await context.Customers.FindAsync(id), "The selected customer has not been deleted.");
        }

        [TestCase(3)]
        [TestCase(-1)]
        public async Task DeleteAsync_NotExistingCustomer_ThrowsArgumentNullException(int id)
        {
            // Arrange
            ApplicationContext context = await UnitTestHelper.GetApplicationContext();
            IRepository<Customer> repo = new CustomerRepository(context);
            
            // Act
            var exception = Assert.CatchAsync<ArgumentNullException>(async () => await repo.DeleteAsync(id));

            // Assert
            Assert.IsNotNull(exception, "The method has not thrown the ArgumentNullException.");
        }
    }
}