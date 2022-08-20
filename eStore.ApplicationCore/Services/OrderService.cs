using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Enums;
using eStore.ApplicationCore.Exceptions;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.DTO;

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
            return await Task.Run(() => _unitOfWork.OrderRepository.Query(o => o.CustomerId == customerId));
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
        }

        public async Task<Order> CreateOrderAsync(int customerId, IEnumerable<IOrderItem> items, IOrderAddress address)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new CustomerNotFoundException($"The customer with id {customerId} has not been found.");
            if (customer.IsDeleted)
                throw new AccountDeactivatedException($"The account with id {customerId} has  been deactivated.");
            customer.ShoppingCart.Goods.Clear();
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);

            var order = new Order
            {
                Customer = customer,
                TimeStamp = DateTime.Now,
                Status = OrderStatus.New,
                OrderItems = new List<OrderItem>(),
                Total = 0,
                ShippingAddress = address.ShippingAddress,
                ShippingCity = address.ShippingCity,
                ShippingPostalCode = address.ShippingPostalCode
            };
            foreach (var orderItem in items)
            {
                var goods = await _unitOfWork.GoodsRepository.GetByIdAsync(orderItem.GoodsId);
                if (goods == null)
                    throw new GoodsNotFoundException($"The goods with the id {orderItem.GoodsId} has not been found.");
                if (goods.IsDeleted)
                    throw new EntityDeletedException($"The goods with the id {orderItem.GoodsId} has been deleted.");
                if (orderItem.Quantity < 1)
                    throw new InvalidQuantityException("The quantity must be greater or equal 1.");

                order.OrderItems.Add(new OrderItem
                    { Order = order, Goods = goods, Quantity = orderItem.Quantity, UnitPrice = goods.Price });
                order.Total += goods.Price * orderItem.Quantity;
            }

            await _unitOfWork.OrderRepository.AddAsync(order);
            var emailAttachment = _attachmentService.CreateOrderInfoPdfAndReturnPath(order);
            await _emailService.SendPurchaseEmailAsyncAsync(order, emailAttachment);
            return order;
        }

        public async Task PayOrder(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new OrderNotFoundException($"The order with the id {orderId} has not been found.");
            if (order.Status >= OrderStatus.Paid)
                throw new StatusChangingException($"Status cannot be changed. Current status {order.Status}");

            order.Status = OrderStatus.Paid;
            await _unitOfWork.OrderRepository.UpdateAsync(order);
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new OrderNotFoundException($"The order with the id {orderId} has not been found.");
            if (order.Status >= OrderStatus.Received)
                throw new StatusChangingException($"Status cannot be changed. Current status {order.Status}");

            order.Status = OrderStatus.Cancelled;
            await _unitOfWork.OrderRepository.UpdateAsync(order);
        }
    }
}