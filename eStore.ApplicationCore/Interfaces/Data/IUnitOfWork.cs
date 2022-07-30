using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces.Data
{
    public interface IUnitOfWork
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
}