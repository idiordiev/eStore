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

namespace eStore.Infrastructure.Tests.Persistence
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new CustomerRepository(_context);
        }

        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IRepository<Customer> _repository;

        [TestCase(1)]
        [TestCase(2)]
        public async Task GetByIdAsync_ExistingCustomer_ReturnsCustomer(int id)
        {
            // Arrange
            Customer expected = _helper.Customers.FirstOrDefault(c => c.Id == id);

            // Act
            Customer actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new CustomerEqualityComparer()),
                "The actual customer is not equal to the expected.");
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(-1)]
        public async Task GetByIdAsync_NotExistingCustomer_ReturnsNull(int id)
        {
            // Arrange

            // Act
            Customer actual = await _repository.GetByIdAsync(id);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null customer.");
        }

        [Test]
        public async Task GetAllAsync_NotEmptyDb_ReturnsCollectionOfCustomers()
        {
            // Arrange
            var expected = _helper.Customers;

            // Act
            var actual = await _repository.GetAllAsync();

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new CustomerEqualityComparer()),
                "The actual collection of customers is not equal to the expected.");
        }

        [Test]
        public Task Query_WithPredicate_ReturnsSuitableCustomers()
        {
            // Arrange
            var expected = _helper.Customers.Where(c => c.Id == 2);

            // Act
            var actual = _repository.Query(c => c.Id == 2);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new CustomerEqualityComparer()),
                "The actual collection of customers is not equal to the expected.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddAsync_NewCustomer_AddsCustomerAndSavesToDb()
        {
            // Arrange
            var newCustomer = new Customer
            {
                IsDeleted = false,
                IdentityId = Guid.NewGuid().ToString(),
                Email = "Email3",
                ShoppingCart = new ShoppingCart()
            };

            // Act
            await _repository.AddAsync(newCustomer);

            // Assert
            Assert.That(_context.Customers.Count(), Is.EqualTo(3),
                "The new customer has not been added to the context.");
            Assert.That(await _context.Customers.FindAsync(3), Is.Not.Null,
                "The new customer has been added with the wrong ID.");
        }

        [Test]
        public async Task UpdateAsync_ExistingCustomer_UpdatesCustomerAndSavesToDb()
        {
            // Arrange
            Customer customer = await _context.Customers.FindAsync(1);

            // Act
            customer.FirstName = "Name1";
            await _repository.UpdateAsync(customer);

            // Assert
            Assert.That((await _context.Customers.FindAsync(customer.Id)).FirstName, Is.EqualTo("Name1"),
                "The customer has not been updated.");
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task DeleteAsync_ExistingCustomer_DeletesCustomerAndSavesToDb(int id)
        {
            // Arrange

            // Act
            await _repository.DeleteAsync(id);

            // Assert
            Assert.That(_context.Customers.Count(), Is.EqualTo(1), "Any customers has not been deleted.");
            Assert.That(await _context.Customers.FindAsync(id), Is.Null, "The selected customer has not been deleted.");
        }

        [TestCase(3)]
        [TestCase(-1)]
        public Task DeleteAsync_NotExistingCustomer_ThrowsArgumentNullException(int id)
        {
            // Arrange

            // Act
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(id));

            // Assert
            Assert.That(exception, Is.Not.Null, "The method has not thrown the ArgumentNullException.");
            return Task.CompletedTask;
        }
    }
}