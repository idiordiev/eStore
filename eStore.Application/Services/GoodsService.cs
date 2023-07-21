using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Exceptions;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Domain.Entities;

namespace eStore.Application.Services;

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
        Customer customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new CustomerNotFoundException($"The customer with the id {customerId} has not been found.");
        }

        if (customer.IsDeleted)
        {
            throw new AccountDeactivatedException(
                $"The account with the id {customerId} has already been deactivated.");
        }

        return customer.ShoppingCart.Goods;
    }

    public async Task<bool> CheckIfAddedToCartAsync(int customerId, int goodsId)
    {
        Customer customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new CustomerNotFoundException($"The customer with the id {customerId} has not been found.");
        }

        if (customer.IsDeleted)
        {
            throw new AccountDeactivatedException(
                $"The account with the id {customerId} has already been deactivated.");
        }

        return customer.ShoppingCart.Goods.Any(g => g.Id == goodsId);
    }
}