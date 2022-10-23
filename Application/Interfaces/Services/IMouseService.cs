using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Application.Filtering.Models;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface IMouseService
    {
        Task<IEnumerable<Mouse>> GetPresentAsync();
        Task<IEnumerable<Mouse>> GetPresentByFilterAsync(MouseFilterModel filter);
        Task<Mouse> GetByIdAsync(int mouseId);
        Task<IEnumerable<string>> GetManufacturersAsync();
        Task<IEnumerable<string>> GetConnectionTypesAsync();
        Task<IEnumerable<string>> GetBacklightsAsync();
    }
}