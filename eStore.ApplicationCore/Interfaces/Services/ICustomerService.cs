using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Customer> RegisterAsync(string email, string password);
        Task<Customer> LoginAsync(string email, string password);
        Task ChangePasswordAsync(string email, string oldPassword, string newPassword);
        Task ChangePersonalInfo(Customer customer);
        Task DeactivateAccountAsync(int customerId);
    }
}