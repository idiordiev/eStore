using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Interfaces.Services
{
    public interface IGoodsService
    {
        Task<IEnumerable<Keyboard>> GetKeyboardsAsync();
        Task<IEnumerable<Keyboard>> GetKeyboardsByFilterAsync(KeyboardFilterModel filter);
        Task<Keyboard> GetKeyboardByIdAsync(int keyboardId);
        Task<IEnumerable<Mouse>> GetMousesAsync();
        Task<IEnumerable<Mouse>> GetMousesByFilterAsync(MouseFilterModel filter);
        Task<Mouse> GetMouseByIdAsync(int mouseId);
        Task<IEnumerable<Mousepad>> GetMousepadsAsync();
        Task<IEnumerable<Mousepad>> GetMousepadsByFilterAsync(MousepadFilterModel filter);
        Task<Mousepad> GetMousepadByIdAsync(int mousepadId);
        Task<IEnumerable<Gamepad>> GetGamepadsAsync();
        Task<IEnumerable<Gamepad>> GetGamepadsByFilterAsync(GamepadFilterModel filter);
        Task<Gamepad> GetGamepadByIdAsync(int gamepadId);
    }
}