using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Application.Filtering.Models;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface IKeyboardService
    {
        Task<IEnumerable<Keyboard>> GetPresentAsync();
        Task<IEnumerable<Keyboard>> GetPresentByFilterAsync(KeyboardFilterModel filter);
        Task<Keyboard> GetByIdAsync(int keyboardId);
        Task<IEnumerable<string>> GetManufacturersAsync();
        Task<IEnumerable<KeyboardSwitch>> GetSwitchesAsync();
        Task<IEnumerable<string>> GetSizesAsync();
        Task<IEnumerable<string>> GetTypesAsync();
        Task<IEnumerable<string>> GetConnectionTypesAsync();
        Task<IEnumerable<string>> GetBacklightsAsync();
        Task<IEnumerable<string>> GetKeyRolloverAsync();
    }
}