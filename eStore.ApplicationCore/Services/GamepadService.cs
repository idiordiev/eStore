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
    public class GamepadService : IGamepadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GamepadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Gamepad>> GetAllAsync()
        {
            return Task.FromResult(_unitOfWork.GamepadRepository.Query(g => !g.IsDeleted));
        }

        public async Task<IEnumerable<Gamepad>> GetAllByFilterAsync(GamepadFilterModel filter)
        {
            var queryExpression = BuildFilterExpression(filter);
            var queryLambda = (Func<Gamepad, bool>)queryExpression.GetLambdaOrNull().Compile();
            return await Task.FromResult(_unitOfWork.GamepadRepository.Query(queryLambda));
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

        private Expression BuildFilterExpression(GamepadFilterModel filter)
        {
            Expression baseExpression = Expression.Constant(true);
            var gamepadParameter = Expression.Parameter(typeof(Gamepad), "g");

            Expression isDeletedProperty = Expression.Property(gamepadParameter, nameof(Gamepad.IsDeleted));
            Expression notDeletedCondition = Expression.IsFalse(isDeletedProperty);
            baseExpression = Expression.AndAlso(baseExpression, notDeletedCondition);

            if (filter.MinPrice != null)
            {
                Expression left = Expression.Property(gamepadParameter, "Price");
                Expression right = Expression.Constant(filter.MinPrice);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.MaxPrice != null)
            {
                Expression left = Expression.Property(gamepadParameter, "Price");
                Expression right = Expression.Constant(filter.MaxPrice);
                Expression expression = Expression.LessThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.ManufacturerIds != null && filter.ManufacturerIds.Any())
            {
                Expression values = Expression.Constant(filter.ManufacturerIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(gamepadParameter, "ManufacturerId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.ConnectionTypeIds != null && filter.ConnectionTypeIds.Any())
            {
                Expression values = Expression.Constant(filter.ConnectionTypeIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(gamepadParameter, "ConnectionTypeId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }
            
            if (filter.FeedbackIds != null && filter.FeedbackIds.Any())
            {
                Expression values = Expression.Constant(filter.FeedbackIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(gamepadParameter, "FeedbackId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            baseExpression = Expression.Lambda(baseExpression, gamepadParameter);
            return baseExpression;
        }
    }
}