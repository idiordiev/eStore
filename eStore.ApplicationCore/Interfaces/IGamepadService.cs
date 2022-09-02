using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IGamepadService
    {
        Task<IEnumerable<Gamepad>> GetPresentAsync();
        Task<IEnumerable<Gamepad>> GetPresentByFilterAsync(GamepadFilterModel filter);
        Task<Gamepad> GetByIdAsync(int gamepadId);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<IEnumerable<Feedback>> GetFeedbacksAsync();
        Task<IEnumerable<ConnectionType>> GetConnectionTypesAsync();
        Task<IEnumerable<CompatibleDevice>> GetCompatibleDevicesAsync();
    }
}