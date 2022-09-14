using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Exceptions;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.DomainServices;

namespace eStore.ApplicationCore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            var existingCustomers = _unitOfWork.CustomerRepository.Query(c => c.Email == customer.Email);
            if (existingCustomers.Any())
                throw new EmailNotUniqueException($"The email {customer.Email} is already used.");

            customer.ShoppingCart = new ShoppingCart();
            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _emailService.SendRegisterEmailAsync(customer);
        }

        public async Task UpdateCustomerInfoAsync(Customer customer)
        {
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        }

        public async Task DeactivateAccountAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            CheckIfCustomerIsPresent(customer);

            customer.IsDeleted = true;
            var email = customer.Email;
            customer.Email = null;
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _emailService.SendDeactivationEmailAsync(email);
        }

        public async Task AddGoodsToCartAsync(int customerId, int goodsId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            CheckIfCustomerIsPresent(customer);

            if (customer.ShoppingCart.Goods.Any(g => g.GoodsId == goodsId))
                throw new GoodsAlreadyAddedException(
                    $"The goods with id {goodsId} is already added to the cart of the customer {customerId}.");

            var goods = await _unitOfWork.GoodsRepository.GetByIdAsync(goodsId);
            CheckIfGoodsIsPresent(goods);

            customer.ShoppingCart.Goods.Add(new GoodsInCart { CartId = customer.ShoppingCart.Id, GoodsId = goods.Id });
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        }

        public async Task RemoveGoodsFromCartAsync(int customerId, int goodsId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            CheckIfCustomerIsPresent(customer);

            var goodsInCart = customer.ShoppingCart.Goods.FirstOrDefault(g => g.GoodsId == goodsId);
            if (goodsInCart == null)
                throw new GoodsNotFoundException(
                    $"The goods with the id {goodsId} has not been found in the cart of the customer {customerId}.");

            customer.ShoppingCart.Goods.Remove(goodsInCart);
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        }

        public async Task ClearCustomerCartAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            CheckIfCustomerIsPresent(customer);

            customer.ShoppingCart.Goods.Clear();
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
        }

        private static void CheckIfCustomerIsPresent(Customer customer)
        {
            if (customer == null) throw new CustomerNotFoundException("The customer has not been found.");

            if (customer.IsDeleted)
                throw new AccountDeactivatedException(
                    $"The account with the id {customer.Id} has already been deactivated.");
        }

        private static void CheckIfGoodsIsPresent(Goods goods)
        {
            if (goods == null) throw new GoodsNotFoundException("The goods has not been found.");

            if (goods.IsDeleted)
                throw new EntityDeletedException($"The goods with the id {goods.Id} has been deleted.");
        }
    }
}