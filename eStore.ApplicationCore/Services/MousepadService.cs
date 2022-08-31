﻿using System;
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
    public class MousepadService : IMousepadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MousepadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Mousepad>> GetPresentAsync()
        {
            return await Task.Run( () => _unitOfWork.MousepadRepository.Query(m => !m.IsDeleted));
        }

        public async Task<IEnumerable<Mousepad>> GetPresentByFilterAsync(MousepadFilterModel filter)
        {
            IFilterExpressionFactory<Mousepad> filterExpressionFactory = new MousepadFilterExpressionFactory();
            var queryExpression = filterExpressionFactory.CreateExpression(filter);
            return await Task.Run(() => _unitOfWork.MousepadRepository.Query(queryExpression));
        }

        public async Task<Mousepad> GetByIdAsync(int mousepadId)
        {
            return await _unitOfWork.MousepadRepository.GetByIdAsync(mousepadId);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            var mousepads = await _unitOfWork.MousepadRepository.GetAllAsync();
            return mousepads.Select(m => m.Manufacturer).Distinct().OrderBy(m => m.Name);
        }

        public async Task<IEnumerable<Material>> GetTopMaterialsAsync()
        {
            var mousepads = await _unitOfWork.MousepadRepository.GetAllAsync();
            return mousepads.Select(m => m.TopMaterial).Distinct().OrderBy(m => m.Id);
        }

        public async Task<IEnumerable<Material>> GetBottomMaterialsAsync()
        {
            var mousepads = await _unitOfWork.MousepadRepository.GetAllAsync();
            return mousepads.Select(m => m.BottomMaterial).Distinct().OrderBy(m => m.Id);
        }
    }
}