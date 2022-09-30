using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Application.FilterModels;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Services
{
    public interface IGamepadService
    {
        Task<IEnumerable<Gamepad>> GetPresentAsync();
        Task<IEnumerable<Gamepad>> GetPresentByFilterAsync(GamepadFilterModel filter);
        Task<Gamepad> GetByIdAsync(int gamepadId);
        Task<IEnumerable<string>> GetManufacturersAsync();
        Task<IEnumerable<string>> GetFeedbacksAsync();
        Task<IEnumerable<string>> GetConnectionTypesAsync();
        Task<IEnumerable<string>> GetCompatibleDevicesAsync();
    }
}