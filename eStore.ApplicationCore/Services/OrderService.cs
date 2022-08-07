using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Enums;
using eStore.ApplicationCore.Exceptions;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.Services;

namespace eStore.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IEmailService emailService, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _attachmentService = attachmentService;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return _unitOfWork.OrderRepository.Query(o => o.CustomerId == customerId);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
        }

        public async Task<Order> CreateOrderAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new UserNotFoundException($"The customer with id {customerId} has not been found.");
            if (customer.IsDeleted)
                throw new AccountDeactivatedException($"The account with id {customerId} has  been deactivated.");

            var order = new Order
            {
                CustomerId = customerId,
                TimeStamp = DateTime.Now,
                Status = OrderStatus.New
            };
            await _unitOfWork.OrderRepository.AddAsync(order);
            return order;
        }

        public async Task AddGoodsToOrderAsync(int orderId, int goodsId, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidQuantityException("The quantity must be greater than 0.");

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException($"The order with the ID #{orderId} has not been found.");

            var goods = await _unitOfWork.GoodsRepository.GetByIdAsync(goodsId);
            if (goods == null)
                throw new GoodsNotFoundException($"The goods with the ID #{goodsId} has not been found.");

            var orderItem = new OrderItem
            {
                GoodsId = goodsId,
                OrderId = orderId,
                Quantity = quantity,
                UnitPrice = goods.Price
            };
            await _unitOfWork.OrderItemRepository.AddAsync(orderItem);
            await RecalculateOrderTotal(orderId);
        }

        public async Task RemoveGoodsFromOrderAsync(int orderId, int goodsId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException($"The order with the ID #{orderId} has not been found.");

            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.GoodsId == goodsId);
            if (orderItem == null)
                throw new GoodsNotFoundException(
                    $"The goods with the ID {goodsId} has not been found in the order {orderId}.");

            await _unitOfWork.OrderItemRepository.DeleteAsync(orderItem.Id);
            await RecalculateOrderTotal(orderId);
        }

        public async Task PayOrder(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (order.Status >= OrderStatus.Paid)
                throw new StatusChangingException($"Status cannot be changed. Current status {order.Status}");

            order.Status = OrderStatus.Paid;
            await _unitOfWork.OrderRepository.UpdateAsync(order);
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (order.Status >= OrderStatus.Received)
                throw new StatusChangingException($"Status cannot be changed. Current status {order.Status}");

            order.Status = OrderStatus.Cancelled;
            await _unitOfWork.OrderRepository.UpdateAsync(order);
        }

        private async Task RecalculateOrderTotal(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException($"The order with the ID #{orderId} has not been found.");

            if (order.OrderItems == null)
                throw new ApplicationException("The collection of the order items is null.");

            var total = order.OrderItems.Sum(orderItem => orderItem.UnitPrice * orderItem.Quantity);
            order.Total = total;
            await _unitOfWork.OrderRepository.UpdateAsync(order);
        }
    }
}