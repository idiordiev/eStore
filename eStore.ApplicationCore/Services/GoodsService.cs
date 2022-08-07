using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;
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

        public Task<IEnumerable<Keyboard>> GetKeyboardsAsync()
        {
            return Task.FromResult(_unitOfWork.KeyboardRepository.Query(k => !k.IsDeleted));
        }

        public async Task<IEnumerable<Keyboard>> GetKeyboardsByFilterAsync(KeyboardFilterModel filter)
        {
            return await Task.FromResult(_unitOfWork.KeyboardRepository.Query(k =>
                k.Price >= filter.MinPrice && k.Price <= filter.MaxPrice
                                           && filter.ManufacturerIds != null && filter.ManufacturerIds.Any()
                    ? filter.ManufacturerIds.Contains(k.ManufacturerId)
                    : true
                      && filter.KeyboardSizeIds != null && filter.KeyboardSizeIds.Any()
                        ? filter.KeyboardSizeIds.Contains(k.SizeId)
                        : true
                          && filter.KeyboardTypeIds != null && filter.KeyboardTypeIds.Any()
                            ? filter.KeyboardTypeIds.Contains(k.TypeId)
                            : true
                              && filter.SwitchIds != null && filter.SwitchIds.Any()
                                ? filter.SwitchIds.Contains(k.SwitchId ?? -1)
                                : true));
        }

        public async Task<Keyboard> GetKeyboardByIdAsync(int keyboardId)
        {
            return await _unitOfWork.KeyboardRepository.GetByIdAsync(keyboardId);
        }

        public Task<IEnumerable<Mouse>> GetMousesAsync()
        {
            return Task.FromResult(_unitOfWork.MouseRepository.Query(m => !m.IsDeleted));
        }

        public async Task<IEnumerable<Mouse>> GetMousesByFilterAsync(MouseFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Mouse> GetMouseByIdAsync(int mouseId)
        {
            return await _unitOfWork.MouseRepository.GetByIdAsync(mouseId);
        }

        public Task<IEnumerable<Mousepad>> GetMousepadsAsync()
        {
            return Task.FromResult(_unitOfWork.MousepadRepository.Query(m => !m.IsDeleted));
        }

        public async Task<IEnumerable<Mousepad>> GetMousepadsByFilterAsync(MousepadFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Mousepad> GetMousepadByIdAsync(int mousepadId)
        {
            return await _unitOfWork.MousepadRepository.GetByIdAsync(mousepadId);
        }

        public Task<IEnumerable<Gamepad>> GetGamepadsAsync()
        {
            return Task.FromResult(_unitOfWork.GamepadRepository.Query(g => !g.IsDeleted));
        }

        public async Task<IEnumerable<Gamepad>> GetGamepadsByFilterAsync(GamepadFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Gamepad> GetGamepadByIdAsync(int gamepadId)
        {
            return await _unitOfWork.GamepadRepository.GetByIdAsync(gamepadId);
        }

        private Expression BuildKeyboardFilterExpression(KeyboardFilterModel filter)
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
                Expression containsExpression = Expression.Call(method, property, values);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.SwitchIds != null && filter.SwitchIds.Any())
            {
                Expression values = Expression.Constant(filter.SwitchIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(keyboardParameter, "SwitchId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, property, values);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.KeyboardSizeIds != null && filter.KeyboardSizeIds.Any())
            {
                Expression values = Expression.Constant(filter.KeyboardSizeIds, typeof(IEnumerable<int>));
                Expression enumProperty = Expression.Property(keyboardParameter, "Size");
                Expression intProperty = Expression.Convert(enumProperty, typeof(int));
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, intProperty, values);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.KeyboardTypeIds != null && filter.KeyboardTypeIds.Any())
            {
                Expression values = Expression.Constant(filter.KeyboardTypeIds, typeof(IEnumerable<int>));
                Expression enumProperty = Expression.Property(keyboardParameter, "Type");
                Expression intProperty = Expression.Convert(enumProperty, typeof(int));
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, intProperty, values);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            baseExpression = Expression.Lambda(baseExpression, keyboardParameter);
            return baseExpression;
        }
    }
}