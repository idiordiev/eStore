using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Exceptions;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace eStore.ApplicationCore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> RegisterAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
                throw new EmailNotUniqueException($"User with email {email} already exists.");

            var existingCustomers = _unitOfWork.CustomerRepository.Query(c => c.Email == email);
            if (existingCustomers.Any())
                throw new EmailNotUniqueException($"Customer with email {email} already exists.");
            
            var newUser = new IdentityUser() { Email = email };
            var result = await _userManager.CreateAsync(newUser);
            if (result.Succeeded)
            {
                var newCustomer = new Customer()
                {
                    IdentityId = newUser.Id,
                    Email = email
                };
                await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
                return newCustomer;
            }
            else
            {
                throw new ApplicationException("Something has gone wrong during creating the Identity User.");
            }
        }

        public async Task<Customer> LoginAsync(string email, string password)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null || await _userManager.CheckPasswordAsync(identityUser, password))
                throw new UserNotFoundException("User with such credentials has not been found.");

            var customer = _unitOfWork.CustomerRepository.Query(c => identityUser.Id == c.IdentityId).First();
            return customer;
        }

        public async Task ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task ChangePersonalInfo(Customer customer)
        {
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        }

        public async Task DeactivateAccountAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            customer.IsDeleted = true;
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        }
    }
}