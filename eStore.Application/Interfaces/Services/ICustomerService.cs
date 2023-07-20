using System.Threading.Tasks;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerInfoAsync(Customer customer);
        Task DeactivateAccountAsync(int customerId);
        Task AddGoodsToCartAsync(int customerId, int goodsId);
        Task RemoveGoodsFromCartAsync(int customerId, int goodsId);
        Task ClearCustomerCartAsync(int customerId);
    }
}