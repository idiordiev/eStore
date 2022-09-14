using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.Infrastructure.Data.Repositories;

namespace eStore.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        private IRepository<Customer> _customerRepository;
        private IRepository<Gamepad> _gamepadRepository;
        private IRepository<Goods> _goodsRepository;
        private IRepository<Keyboard> _keyboardRepository;
        private IRepository<Mousepad> _mousepadRepository;
        private IRepository<Mouse> _mouseRepository;
        private IRepository<OrderItem> _orderItemRepository;
        private IRepository<Order> _orderRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IRepository<Customer> CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_context);
                return _customerRepository;
            }
        }

        public IRepository<Goods> GoodsRepository
        {
            get
            {
                if (_goodsRepository == null)
                    _goodsRepository = new GoodsRepository(_context);
                return _goodsRepository;
            }
        }

        public IRepository<Gamepad> GamepadRepository
        {
            get
            {
                if (_gamepadRepository == null)
                    _gamepadRepository = new GamepadRepository(_context);
                return _gamepadRepository;
            }
        }

        public IRepository<Keyboard> KeyboardRepository
        {
            get
            {
                if (_keyboardRepository == null)
                    _keyboardRepository = new KeyboardRepository(_context);
                return _keyboardRepository;
            }
        }

        public IRepository<Mouse> MouseRepository
        {
            get
            {
                if (_mouseRepository == null)
                    _mouseRepository = new MouseRepository(_context);
                return _mouseRepository;
            }
        }

        public IRepository<Mousepad> MousepadRepository
        {
            get
            {
                if (_mousepadRepository == null)
                    _mousepadRepository = new MousepadRepository(_context);
                return _mousepadRepository;
            }
        }

        public IRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }

        public IRepository<OrderItem> OrderItemRepository
        {
            get
            {
                if (_orderItemRepository == null)
                    _orderItemRepository = new OrderItemRepository(_context);
                return _orderItemRepository;
            }
        }
        
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) _context.Dispose();

                _disposed = true;
            }
        }
    }
}