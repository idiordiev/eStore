using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
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
        
        public Task<IEnumerable<Keyboard>> GetAllAsync()
        {
            return Task.FromResult(_unitOfWork.KeyboardRepository.Query(k => !k.IsDeleted));
        }

        public async Task<IEnumerable<Keyboard>> GetAllByFilterAsync(KeyboardFilterModel filter)
        {
            var queryExpression = BuildFilterExpression(filter);
            var queryLambda = (Func<Keyboard, bool>)queryExpression.GetLambdaOrNull().Compile();
            return await Task.FromResult(_unitOfWork.KeyboardRepository.Query(queryLambda));
        }
        
        private Expression BuildFilterExpression(KeyboardFilterModel filter)
        {
            Expression baseExpression = Expression.Constant(true);
            var keyboardParameter = Expression.Parameter(typeof(Keyboard), "k");

            Expression isDeletedProperty = Expression.Property(keyboardParameter, nameof(Keyboard.IsDeleted));
            Expression notDeletedCondition = Expression.IsFalse(isDeletedProperty);
            baseExpression = Expression.AndAlso(baseExpression, notDeletedCondition);

            if (filter.MinPrice != null)
            {
                Expression left = Expression.Property(keyboardParameter, "Price");
                Expression right = Expression.Constant(filter.MinPrice);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.MaxPrice != null)
            {
                Expression left = Expression.Property(keyboardParameter, "Price");
                Expression right = Expression.Constant(filter.MaxPrice);
                Expression expression = Expression.LessThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.ManufacturerIds != null && filter.ManufacturerIds.Any())
            {
                Expression values = Expression.Constant(filter.ManufacturerIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(keyboardParameter, "ManufacturerId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.SwitchIds != null && filter.SwitchIds.Any())
            {
                Expression values = Expression.Constant(filter.SwitchIds, typeof(IEnumerable<int?>));
                Expression property = Expression.Property(keyboardParameter, "SwitchId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int?));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.KeyboardSizeIds != null && filter.KeyboardSizeIds.Any())
            {
                Expression values = Expression.Constant(filter.KeyboardSizeIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(keyboardParameter, "SizeId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.KeyboardTypeIds != null && filter.KeyboardTypeIds.Any())
            {
                Expression values = Expression.Constant(filter.KeyboardTypeIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(keyboardParameter, "TypeId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            baseExpression = Expression.Lambda(baseExpression, keyboardParameter);
            return baseExpression;
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