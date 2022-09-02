using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Exceptions;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;

namespace eStore.ApplicationCore.Services
{
    public class GoodsService : IGoodsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GoodsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Goods>> GetAllAsync()
        {
            return await _unitOfWork.GoodsRepository.GetAllAsync();
        }

        public async Task<Goods> GetByIdAsync(int goodsId)
        {
            return await _unitOfWork.GoodsRepository.GetByIdAsync(goodsId);
        }

        public async Task<IEnumerable<Goods>> GetGoodsInCustomerCartAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new CustomerNotFoundException($"The customer with the id {customerId} has not been found.");
            if (customer.IsDeleted)
                throw new AccountDeactivatedException(
                    $"The account with the id {customerId} has already been deactivated.");

            return customer.ShoppingCart.Goods.Select(g => g.Goods);
        }

        public async Task<bool> CheckIfAddedToCartAsync(int customerId, int goodsId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new CustomerNotFoundException($"The customer with the id {customerId} has not been found.");
            if (customer.IsDeleted)
                throw new AccountDeactivatedException(
                    $"The account with the id {customerId} has already been deactivated.");

            return customer.ShoppingCart.Goods.Any(g => g.GoodsId == goodsId);
        }
    }
}