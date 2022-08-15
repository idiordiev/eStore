using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IMousepadService
    {
        Task<IEnumerable<Mousepad>> GetAllAsync();
        Task<IEnumerable<Mousepad>> GetAllByFilterAsync(MousepadFilterModel filter);
        Task<Mousepad> GetByIdAsync(int mousepadId);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<IEnumerable<Material>> GetTopMaterialsAsync();
        Task<IEnumerable<Material>> GetBottomMaterialsAsync();
    }
}