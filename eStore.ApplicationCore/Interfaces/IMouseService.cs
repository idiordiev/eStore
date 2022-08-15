using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IMouseService
    {
        Task<IEnumerable<Mouse>> GetAllAsync();
        Task<IEnumerable<Mouse>> GetAllByFilterAsync(MouseFilterModel filter);
        Task<Mouse> GetByIdAsync(int mouseId);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
    }
}