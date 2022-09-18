using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Application.Exceptions;
using eStore.Application.Interfaces;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Application.Interfaces.DTO;
using eStore.Domain.Entities;
using eStore.Domain.Enums;

namespace eStore.Application.Services
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
            CheckIfCustomerIsPresent(customer);

            var order = new Order
            {
                Customer = customer,
                TimeStamp = DateTime.Now,
                Status = OrderStatus.New,
                OrderItems = new List<OrderItem>(),
                Total = 0,
                ShippingCountry = address.ShippingCountry,
                ShippingAddress = address.ShippingAddress,
                ShippingCity = address.ShippingCity,
                ShippingPostalCode = address.ShippingPostalCode
            };
            foreach (var orderItem in items)
            {
                var goods = await _unitOfWork.GoodsRepository.GetByIdAsync(orderItem.GoodsId);

                CheckIfGoodsIsPresent(goods);
                if (orderItem.Quantity < 1)
                    throw new InvalidQuantityException("The quantity must be greater or equal 1.");

                order.OrderItems.Add(new OrderItem
                    { Order = order, Goods = goods, Quantity = orderItem.Quantity, UnitPrice = goods.Price });
                order.Total += goods.Price * orderItem.Quantity;
            }

            await _unitOfWork.OrderRepository.AddAsync(order);
            var emailAttachment = _attachmentService.CreateInvoice(order);
            await _emailService.SendPurchaseEmailAsyncAsync(order, emailAttachment);
            return order;
        }

        public async Task PayOrderAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new OrderNotFoundException($"The order with the id {orderId} has not been found.");
            if (order.Status >= OrderStatus.Paid)
                throw new StatusUnchangeableException($"Status cannot be changed. Current status {order.Status}");

            order.Status = OrderStatus.Paid;
            await _unitOfWork.OrderRepository.UpdateAsync(order);
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new OrderNotFoundException($"The order with the id {orderId} has not been found.");
            if (order.Status >= OrderStatus.Received)
                throw new StatusUnchangeableException($"Status cannot be changed. Current status {order.Status}");

            order.Status = OrderStatus.Cancelled;
            await _unitOfWork.OrderRepository.UpdateAsync(order);
        }

        private static void CheckIfCustomerIsPresent(Customer customer)
        {
            if (customer == null) 
                throw new CustomerNotFoundException("The customer has not been found.");

            if (customer.IsDeleted)
                throw new AccountDeactivatedException(
                    $"The account with the id {customer.Id} has already been deactivated.");
        }

        private static void CheckIfGoodsIsPresent(Goods goods)
        {
            if (goods == null) 
                throw new GoodsNotFoundException("The goods has not been found.");

            if (goods.IsDeleted)
                throw new EntityDeletedException($"The goods with the id {goods.Id} has been deleted.");
        }
    }
}