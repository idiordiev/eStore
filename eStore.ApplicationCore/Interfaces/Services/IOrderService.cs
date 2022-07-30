using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> CreateOrderAsync(int customerId);
        Task AddGoodsToOrderAsync(int orderId, int goodsId, int quantity);
        Task RemoveGoodsFromOrderAsync(int orderId, int goodsId);
        Task PayOrder(int orderId);
        Task CancelOrderAsync(int orderId);
    }
}