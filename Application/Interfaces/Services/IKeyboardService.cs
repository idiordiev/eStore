using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Application.FilterModels;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface IKeyboardService
    {
        Task<IEnumerable<Keyboard>> GetPresentAsync();
        Task<IEnumerable<Keyboard>> GetPresentByFilterAsync(KeyboardFilterModel filter);
        Task<Keyboard> GetByIdAsync(int keyboardId);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<IEnumerable<KeyboardSwitch>> GetSwitchesAsync();
        Task<IEnumerable<KeyboardSize>> GetSizesAsync();
        Task<IEnumerable<KeyboardType>> GetTypesAsync();
        Task<IEnumerable<ConnectionType>> GetConnectionTypesAsync();
        Task<IEnumerable<Backlight>> GetBacklightsAsync();
        Task<IEnumerable<KeyRollover>> GetKeyRolloverAsync();
    }
}