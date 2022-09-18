using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Application.FilterModels;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface IMouseService
    {
        Task<IEnumerable<Mouse>> GetPresentAsync();
        Task<IEnumerable<Mouse>> GetPresentByFilterAsync(MouseFilterModel filter);
        Task<Mouse> GetByIdAsync(int mouseId);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<IEnumerable<ConnectionType>> GetConnectionTypesAsync();
        Task<IEnumerable<Backlight>> GetBacklightsAsync();
    }
}