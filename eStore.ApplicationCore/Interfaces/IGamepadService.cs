using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IGamepadService
    {
        Task<IEnumerable<Gamepad>> GetAllAsync();
        Task<IEnumerable<Gamepad>> GetAllByFilterAsync(GamepadFilterModel filter);
        Task<Gamepad> GetByIdAsync(int gamepadId);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<IEnumerable<Feedback>> GetFeedbacksAsync();
        Task<IEnumerable<ConnectionType>> GetConnectionTypesAsync();
    }
}