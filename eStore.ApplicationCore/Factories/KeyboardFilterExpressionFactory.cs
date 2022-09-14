using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;

namespace eStore.ApplicationCore.Factories
{
    internal class KeyboardFilterExpressionFactory : IFilterExpressionFactory<Keyboard>
    {
        public Expression<Func<Keyboard, bool>> CreateExpression(GoodsFilterModel filterModel)
        {
            if (filterModel is not KeyboardFilterModel filter)
                throw new ArgumentException("This factory can only accept the KeyboardFilterModel.");

            var keyboardParameter = Expression.Parameter(typeof(Keyboard), "k");
            var filterExpression = GetBaseExpression(keyboardParameter);
            AddMinPriceConstraint(ref filterExpression, keyboardParameter, filter.MinPrice);
            AddMaxPriceConstraint(ref filterExpression, keyboardParameter, filter.MaxPrice);
            AddManufacturerConstraint(ref filterExpression, keyboardParameter, filter.ManufacturerIds);
            AddConnectionTypeConstraint(ref filterExpression, keyboardParameter, filter.ConnectionTypeIds);
            AddSwitchConstraint(ref filterExpression, keyboardParameter, filter.SwitchIds);
            AddKeyboardSizeConstraint(ref filterExpression, keyboardParameter, filter.KeyboardSizeIds);
            AddKeyboardTypeConstraint(ref filterExpression, keyboardParameter, filter.KeyboardTypeIds);
            AddKeyRolloverConstraint(ref filterExpression, keyboardParameter, filter.KeyRolloverIds);
            AddBacklightConstraint(ref filterExpression, keyboardParameter, filter.BacklightIds);

            return Expression.Lambda<Func<Keyboard, bool>>(filterExpression, keyboardParameter);
        }

        private Expression GetBaseExpression(ParameterExpression parameter)
        {
            Expression isDeletedProperty = Expression.Property(parameter, nameof(Keyboard.IsDeleted));
            Expression baseExpression = Expression.IsFalse(isDeletedProperty);
            return baseExpression;
        }

        private void AddMinPriceConstraint(ref Expression baseExpression, ParameterExpression parameter, decimal? price)
        {
            if (price == null)
                return;

            Expression left = Expression.Property(parameter, nameof(Keyboard.Price));
            Expression right = Expression.Constant(price.Value);
            Expression expression = Expression.GreaterThanOrEqual(left, right);
            baseExpression = Expression.AndAlso(baseExpression, expression);
        }

        private void AddMaxPriceConstraint(ref Expression baseExpression, ParameterExpression parameter, decimal? price)
        {
            if (price == null)
                return;

            Expression left = Expression.Property(parameter, nameof(Keyboard.Price));
            Expression right = Expression.Constant(price.Value);
            Expression expression = Expression.LessThanOrEqual(left, right);
            baseExpression = Expression.AndAlso(baseExpression, expression);
        }

        private void AddManufacturerConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> manufacturerIds)
        {
            if (manufacturerIds == null || !manufacturerIds.Any())
                return;

            Expression values = Expression.Constant(manufacturerIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Keyboard.ManufacturerId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddConnectionTypeConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> connectionTypeIds)
        {
            if (connectionTypeIds == null || !connectionTypeIds.Any()) 
                return;

            Expression values = Expression.Constant(connectionTypeIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Keyboard.ConnectionTypeId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddSwitchConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int?> switchIds)
        {
            if (switchIds == null || !switchIds.Any()) 
                return;

            Expression values = Expression.Constant(switchIds, typeof(IEnumerable<int?>));
            Expression property = Expression.Property(parameter, nameof(Keyboard.SwitchId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int?));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddKeyboardSizeConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> sizeIds)
        {
            if (sizeIds == null || !sizeIds.Any()) 
                return;

            Expression values = Expression.Constant(sizeIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Keyboard.SizeId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddKeyboardTypeConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> typeIds)
        {
            if (typeIds == null || !typeIds.Any()) 
                return;

            Expression values = Expression.Constant(typeIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Keyboard.TypeId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddKeyRolloverConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> rolloverIds)
        {
            if (rolloverIds == null || !rolloverIds.Any())
                return;

            Expression values = Expression.Constant(rolloverIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Keyboard.KeyRolloverId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddBacklightConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> backlightIds)
        {
            if (backlightIds == null || !backlightIds.Any())
                return;

            Expression values = Expression.Constant(backlightIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Keyboard.BacklightId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }
    }
}