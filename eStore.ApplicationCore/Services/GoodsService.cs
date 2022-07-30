using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.Services;

namespace eStore.ApplicationCore.Services
{
    public class GoodsService : IGoodsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GoodsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Keyboard>> GetKeyboardsAsync()
        {
            return await _unitOfWork.KeyboardRepository.GetAllAsync();
        }

        public async Task<Keyboard> GetKeyboardByIdAsync(int keyboardId)
        {
            return await _unitOfWork.KeyboardRepository.GetByIdAsync(keyboardId);
        }

        public async Task<IEnumerable<Mouse>> GetMousesAsync()
        {
            return await _unitOfWork.MouseRepository.GetAllAsync();
        }

        public async Task<Mouse> GetMouseByIdAsync(int mouseId)
        {
            return await _unitOfWork.MouseRepository.GetByIdAsync(mouseId);
        }

        public async Task<IEnumerable<Mousepad>> GetMousepadsAsync()
        {
            return await _unitOfWork.MousepadRepository.GetAllAsync();
        }

        public async Task<Mousepad> GetMousepadByIdAsync(int mousepadId)
        {
            return await _unitOfWork.MousepadRepository.GetByIdAsync(mousepadId);
        }

        public async Task<IEnumerable<Gamepad>> GetGamepadsAsync()
        {
            return await _unitOfWork.GamepadRepository.GetAllAsync();
        }

        public async Task<Gamepad> GetGamepadByIdAsync(int gamepadId)
        {
            return await _unitOfWork.GamepadRepository.GetByIdAsync(gamepadId);
        }
    }
}