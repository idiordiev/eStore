using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces.Services
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Customer> GetCustomerByIdentityAsync(string identityId);
        Task<bool> CheckIfAccountNotDeleted(int customerId);
        Task ChangePersonalInfo(Customer customer);
        Task DeactivateAccountAsync(int customerId);
    }
}