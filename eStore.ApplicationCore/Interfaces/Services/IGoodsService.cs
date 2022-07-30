using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces.Services
{
    public interface IGoodsService
    {
        Task<IEnumerable<Keyboard>> GetKeyboardsAsync();
        Task<Keyboard> GetKeyboardByIdAsync(int keyboardId);
        Task<IEnumerable<Mouse>> GetMousesAsync();
        Task<Mouse> GetMouseByIdAsync(int mouseId);
        Task<IEnumerable<Mousepad>> GetMousepadsAsync();
        Task<Mousepad> GetMousepadByIdAsync(int mousepadId);
        Task<IEnumerable<Gamepad>> GetGamepadsAsync();
        Task<Gamepad> GetGamepadByIdAsync(int gamepadId);
    }
}