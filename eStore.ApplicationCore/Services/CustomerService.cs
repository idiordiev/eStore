using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Exceptions;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.Services;

namespace eStore.ApplicationCore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _emailService.SendHtmlEmailAsync(customer.Email, "Welcome to our store!", "text");
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        }

        public async Task<Customer> GetCustomerByIdentityAsync(string identityId)
        {
            var customers = _unitOfWork.CustomerRepository.Query(c => c.IdentityId == identityId).ToList();
            if (!customers.Any())
                throw new UserNotFoundException($"The customer related with Identity {identityId} has not been found.");
            if (customers.Count > 1)
                throw new ApplicationException($"There is multiple customers with Identity {identityId}.");

            return customers.First();
        }

        public async Task<bool> CheckIfAccountNotDeleted(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new UserNotFoundException($"The customer with id {customerId} has not been found.");
            return customer.IsDeleted;
        }

        public async Task ChangePersonalInfo(Customer customer)
        {
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        }

        public async Task DeactivateAccountAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new UserNotFoundException($"The customer with id {customerId} has not been found.");
            if (customer.IsDeleted)
                throw new AccountDeactivatedException(
                    $"The account with id {customerId} has already been deactivated.");

            customer.IsDeleted = true;
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _emailService.SendHtmlEmailAsync(customer.Email, "Your account has been deactivated.", "text");
        }
    }
}