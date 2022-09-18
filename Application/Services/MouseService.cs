using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Factories;
using eStore.Application.FilterModels;
using eStore.Application.Interfaces;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Domain.Entities;

namespace eStore.Application.Services
{
    public class MouseService : IMouseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MouseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Mouse>> GetPresentAsync()
        {
            return await Task.Run(() => _unitOfWork.MouseRepository.Query(m => !m.IsDeleted));
        }

        public async Task<IEnumerable<Mouse>> GetPresentByFilterAsync(MouseFilterModel filter)
        {
            IFilterExpressionFactory<Mouse> filterExpressionFactory = new MouseFilterExpressionFactory();
            var queryExpression = filterExpressionFactory.CreateExpression(filter);
            return await Task.Run(() => _unitOfWork.MouseRepository.Query(queryExpression));
        }

        public async Task<Mouse> GetByIdAsync(int mouseId)
        {
            return await _unitOfWork.MouseRepository.GetByIdAsync(mouseId);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            var mouses = await _unitOfWork.MouseRepository.GetAllAsync();
            return mouses.Select(m => m.Manufacturer).Distinct().OrderBy(m => m.Name);
        }

        public async Task<IEnumerable<ConnectionType>> GetConnectionTypesAsync()
        {
            var mouses = await _unitOfWork.MouseRepository.GetAllAsync();
            return mouses.Select(m => m.ConnectionType).Distinct().OrderBy(c => c.Id);
        }

        public async Task<IEnumerable<Backlight>> GetBacklightsAsync()
        {
            var mouses = await _unitOfWork.MouseRepository.GetAllAsync();
            return mouses.Select(m => m.Backlight).Distinct().OrderBy(b => b.Id);
        }
    }
}