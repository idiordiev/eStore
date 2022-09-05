using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Factories;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.DomainServices;

namespace eStore.ApplicationCore.Services
{
    public class GamepadService : IGamepadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GamepadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Gamepad>> GetPresentAsync()
        {
            return await Task.Run(() => _unitOfWork.GamepadRepository.Query(g => !g.IsDeleted));
        }

        public async Task<IEnumerable<Gamepad>> GetPresentByFilterAsync(GamepadFilterModel filter)
        {
            IFilterExpressionFactory<Gamepad> filterExpressionFactory = new GamepadFilterExpressionFactory();
            var queryExpression = filterExpressionFactory.CreateExpression(filter);
            return await Task.Run(() => _unitOfWork.GamepadRepository.Query(queryExpression));
        }

        public async Task<Gamepad> GetByIdAsync(int gamepadId)
        {
            return await _unitOfWork.GamepadRepository.GetByIdAsync(gamepadId);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            var gamepads = await _unitOfWork.GamepadRepository.GetAllAsync();
            return gamepads.Select(g => g.Manufacturer).Distinct().OrderBy(m => m.Name);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksAsync()
        {
            var gamepads = await _unitOfWork.GamepadRepository.GetAllAsync();
            return gamepads.Select(g => g.Feedback).Distinct().OrderBy(f => f.Id);
        }

        public async Task<IEnumerable<ConnectionType>> GetConnectionTypesAsync()
        {
            var gamepads = await _unitOfWork.GamepadRepository.GetAllAsync();
            return gamepads.Select(g => g.ConnectionType).Distinct().OrderBy(t => t.Id);
        }

        public async Task<IEnumerable<CompatibleDevice>> GetCompatibleDevicesAsync()
        {
            var gamepads = await _unitOfWork.GamepadRepository.GetAllAsync();
            return gamepads.SelectMany(g => g.CompatibleDevices).Select(d => d.CompatibleDevice).Distinct()
                .OrderBy(t => t.Id);
        }
    }
}