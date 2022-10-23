using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Application.Filtering.Models;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface IMousepadService
    {
        Task<IEnumerable<Mousepad>> GetPresentAsync();
        Task<IEnumerable<Mousepad>> GetPresentByFilterAsync(MousepadFilterModel filter);
        Task<Mousepad> GetByIdAsync(int mousepadId);
        Task<IEnumerable<string>> GetManufacturersAsync();
        Task<IEnumerable<string>> GetTopMaterialsAsync();
        Task<IEnumerable<string>> GetBottomMaterialsAsync();
        Task<IEnumerable<string>> GetBacklightsAsync();
    }
}