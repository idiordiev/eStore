using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Filtering.Factories;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Domain.Entities;

namespace eStore.Application.Services
{
    public class KeyboardService : IKeyboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KeyboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Keyboard>> GetPresentAsync()
        {
            return await Task.FromResult(_unitOfWork.KeyboardRepository.Query(k => !k.IsDeleted));
        }

        public async Task<IEnumerable<Keyboard>> GetPresentByFilterAsync(KeyboardFilterModel filter)
        {
            IFilterExpressionFactory<Keyboard, KeyboardFilterModel> filterExpressionFactory =
                new KeyboardFilterExpressionFactory();
            var queryExpression = filterExpressionFactory.GetExpression(filter);
            return await Task.FromResult(_unitOfWork.KeyboardRepository.Query(queryExpression));
        }

        public async Task<Keyboard> GetByIdAsync(int keyboardId)
        {
            return await _unitOfWork.KeyboardRepository.GetByIdAsync(keyboardId);
        }

        public async Task<IEnumerable<string>> GetManufacturersAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Manufacturer).Distinct().OrderBy(m => m);
        }

        public async Task<IEnumerable<KeyboardSwitch>> GetSwitchesAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Switch).Where(sw => sw != null).Distinct().OrderBy(s => s.Name);
        }

        public async Task<IEnumerable<string>> GetSizesAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Size).Distinct();
        }

        public async Task<IEnumerable<string>> GetTypesAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Type).Distinct();
        }

        public async Task<IEnumerable<string>> GetConnectionTypesAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.ConnectionType).Distinct();
        }

        public async Task<IEnumerable<string>> GetBacklightsAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Backlight).Distinct();
        }

        public async Task<IEnumerable<string>> GetKeyRolloverAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.KeyRollover).Distinct();
        }
    }
}