using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;

namespace eStore.ApplicationCore.Factories
{
    internal class GamepadFilterExpressionFactory : IFilterExpressionFactory<Gamepad>
    {
        public Expression<Func<Gamepad, bool>> CreateExpression(GoodsFilterModel filterModel)
        {
            if (filterModel is not GamepadFilterModel filter)
                throw new ArgumentException("This factory can only accept the GamepadFilterModel.");

            var gamepadParameter = Expression.Parameter(typeof(Gamepad), "g");

            var filterExpression = GetBaseExpression(gamepadParameter);
            AddMinPriceConstraint(ref filterExpression, gamepadParameter, filter.MinPrice);
            AddMaxPriceConstraint(ref filterExpression, gamepadParameter, filter.MaxPrice);
            AddManufacturerConstraint(ref filterExpression, gamepadParameter, filter.ManufacturerIds);
            AddConnectionTypeConstraint(ref filterExpression, gamepadParameter, filter.ConnectionTypeIds);
            AddFeedbackConstraint(ref filterExpression, gamepadParameter, filter.FeedbackIds);
            AddCompatibleDevicesConstraint(ref filterExpression, gamepadParameter, filter.CompatibleDevicesIds);

            return Expression.Lambda<Func<Gamepad, bool>>(filterExpression, gamepadParameter);
        }

        private Expression GetBaseExpression(ParameterExpression parameter)
        {
            Expression isDeletedProperty = Expression.Property(parameter, nameof(Gamepad.IsDeleted));
            Expression baseExpression = Expression.IsFalse(isDeletedProperty);
            return baseExpression;
        }

        private void AddMinPriceConstraint(ref Expression baseExpression, ParameterExpression parameter, decimal? price)
        {
            if (price == null)
                return;

            Expression left = Expression.Property(parameter, nameof(Gamepad.Price));
            Expression right = Expression.Constant(price.Value);
            Expression expression = Expression.GreaterThanOrEqual(left, right);
            baseExpression = Expression.AndAlso(baseExpression, expression);
        }

        private void AddMaxPriceConstraint(ref Expression baseExpression, ParameterExpression parameter, decimal? price)
        {
            if (price == null)
                return;

            Expression left = Expression.Property(parameter, nameof(Gamepad.Price));
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
            Expression property = Expression.Property(parameter, nameof(Gamepad.ManufacturerId));
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
            Expression property = Expression.Property(parameter, nameof(Gamepad.ConnectionTypeId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddFeedbackConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> feedbackIds)
        {
            if (feedbackIds == null || !feedbackIds.Any())
                return;

            Expression values = Expression.Constant(feedbackIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Gamepad.FeedbackId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddCompatibleDevicesConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> deviceIds)
        {
            if (deviceIds == null || !deviceIds.Any())
                return;

            // LINQ expression: gamepads.Where(g => g.CompatibleDevice.Select(d => d.CompatibleDeviceId).Distinct.Intersect(deviceIds).Any());
            Expression deviceIdValues = Expression.Constant(deviceIds, typeof(IEnumerable<int>));
            Expression compatibleDevicesProperty = Expression.Property(parameter, nameof(Gamepad.CompatibleDevices));
            var selectMethod = typeof(Enumerable)
                .GetMethods()
                .First(x => x.Name == "Select")
                .MakeGenericMethod(typeof(GamepadCompatibleDevice), typeof(int));
            var distinctMethod = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Distinct")
                .Single(x => x.GetParameters().Length == 1)
                .MakeGenericMethod(typeof(int));
            var intersectMethod = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Intersect")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            var anyMethod = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Any")
                .Single(x => x.GetParameters().Length == 1)
                .MakeGenericMethod(typeof(int));

            var compatibleDeviceParameter = Expression.Parameter(typeof(GamepadCompatibleDevice), "d");
            Expression deviceIdProperty = Expression.Property(compatibleDeviceParameter,
                nameof(GamepadCompatibleDevice.CompatibleDeviceId));
            var selectInnerExpression =
                Expression.Lambda<Func<GamepadCompatibleDevice, int>>(deviceIdProperty, compatibleDeviceParameter);

            Expression selectCall = Expression.Call(selectMethod, compatibleDevicesProperty, selectInnerExpression);
            Expression distinctCall = Expression.Call(distinctMethod, selectCall);
            Expression intersectCall = Expression.Call(intersectMethod, distinctCall, deviceIdValues);
            Expression anyCall = Expression.Call(anyMethod, intersectCall);

            baseExpression = Expression.AndAlso(baseExpression, anyCall);
        }
    }
}