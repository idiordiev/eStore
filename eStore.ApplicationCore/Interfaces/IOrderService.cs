using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.DTO;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> CreateOrderAsync(int customerId, IEnumerable<IOrderItem> items, IOrderAddress address);
        Task PayOrder(int orderId);
        Task CancelOrderAsync(int orderId);
    }
}