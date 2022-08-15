using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task UpdateCustomerInfoAsync(Customer customer);
        Task DeactivateAccountAsync(int customerId);
        Task AddGoodsToCartAsync(int customerId, int goodsId);
        Task RemoveGoodsFromCartAsync(int customerId, int goodsId);
    }
}