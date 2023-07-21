using System;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Data;

public interface IUnitOfWork : IDisposable
{
    IRepository<Customer> CustomerRepository { get; }
    IRepository<Goods> GoodsRepository { get; }
    IRepository<Gamepad> GamepadRepository { get; }
    IRepository<Keyboard> KeyboardRepository { get; }
    IRepository<Mouse> MouseRepository { get; }
    IRepository<Mousepad> MousepadRepository { get; }
    IRepository<Order> OrderRepository { get; }
    IRepository<OrderItem> OrderItemRepository { get; }
}