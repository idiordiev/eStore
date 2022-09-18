using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface IGoodsService
    {
        Task<IEnumerable<Goods>> GetAllAsync();
        Task<Goods> GetByIdAsync(int goodsId);
        Task<IEnumerable<Goods>> GetGoodsInCustomerCartAsync(int customerId);
        Task<bool> CheckIfAddedToCartAsync(int customerId, int goodsId);
    }
}