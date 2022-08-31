using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Factories;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace eStore.ApplicationCore.Services
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
    }
}