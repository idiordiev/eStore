using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Factory;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace eStore.ApplicationCore.Services
{
    public class KeyboardService : IKeyboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KeyboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Keyboard>> GetAllAsync()
        {
            return await Task.Run(() => _unitOfWork.KeyboardRepository.Query(k => !k.IsDeleted));
        }

        public async Task<IEnumerable<Keyboard>> GetAllByFilterAsync(KeyboardFilterModel filter)
        {
            IExpressionFactory expressionFactory = new KeyboardExpressionFactory();
            var queryExpression = expressionFactory.CreateFilterExpression(filter);
            var queryLambda = (Func<Keyboard, bool>)queryExpression.GetLambdaOrNull().Compile();
            return await Task.Run(() => _unitOfWork.KeyboardRepository.Query(queryLambda));
        }

        public async Task<Keyboard> GetByIdAsync(int keyboardId)
        {
            return await _unitOfWork.KeyboardRepository.GetByIdAsync(keyboardId);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Manufacturer).Distinct().OrderBy(m => m.Name);
        }

        public async Task<IEnumerable<KeyboardSwitch>> GetSwitchesAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Switch).Where(sw => sw != null).Distinct().OrderBy(s => s.Name);
        }

        public async Task<IEnumerable<KeyboardSize>> GetSizesAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Size).Distinct().OrderBy(s => s.Id);
        }

        public async Task<IEnumerable<KeyboardType>> GetTypesAsync()
        {
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllAsync();
            return keyboards.Select(k => k.Type).Distinct().OrderBy(t => t.Id);
        }
    }
}