using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Application.Interfaces.DTO;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> CreateOrderAsync(int customerId, IEnumerable<IOrderItem> items, IOrderAddress address);
        Task PayOrderAsync(int orderId);
        Task CancelOrderAsync(int orderId);
    }
}