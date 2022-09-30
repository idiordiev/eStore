using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Application.FilterModels;
using eStore.Application.FilterModels.Shared;
using eStore.Application.Interfaces;
using eStore.Domain.Entities;

namespace eStore.Application.Factories
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
            AddManufacturerConstraint(ref filterExpression, gamepadParameter, filter.Manufacturers);
            AddConnectionTypeConstraint(ref filterExpression, gamepadParameter, filter.ConnectionTypes);
            AddFeedbackConstraint(ref filterExpression, gamepadParameter, filter.Feedbacks);
            AddCompatibleDevicesConstraint(ref filterExpression, gamepadParameter, filter.CompatibleDevices);

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
            IEnumerable<string> manufacturers)
        {
            if (manufacturers == null || !manufacturers.Any())
                return;

            Expression values = Expression.Constant(manufacturers, typeof(IEnumerable<string>));
            Expression property = Expression.Property(parameter, nameof(Gamepad.Manufacturer));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(string));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddConnectionTypeConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<string> connectionTypes)
        {
            if (connectionTypes == null || !connectionTypes.Any())
                return;

            Expression values = Expression.Constant(connectionTypes, typeof(IEnumerable<string>));
            Expression property = Expression.Property(parameter, nameof(Gamepad.ConnectionType));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(string));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddFeedbackConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<string> feedbacks)
        {
            if (feedbacks == null || !feedbacks.Any())
                return;

            Expression values = Expression.Constant(feedbacks, typeof(IEnumerable<string>));
            Expression property = Expression.Property(parameter, nameof(Gamepad.Feedback));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(string));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddCompatibleDevicesConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<string> devices)
        {
            if (devices == null || !devices.Any())
                return;

            // LINQ expression: gamepads.Where(g => g.CompatibleDevices.Select(d => d).Distinct.Intersect(devices).Any());
            Expression deviceIdValues = Expression.Constant(devices, typeof(IEnumerable<string>));
            Expression compatibleDevicesProperty = Expression.Property(parameter, nameof(Gamepad.CompatibleDevices));
            var selectMethod = typeof(Enumerable)
                .GetMethods()
                .First(x => x.Name == "Select")
                .MakeGenericMethod(typeof(string), typeof(string));
            var distinctMethod = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Distinct")
                .Single(x => x.GetParameters().Length == 1)
                .MakeGenericMethod(typeof(string));
            var intersectMethod = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Intersect")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(string));
            var anyMethod = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Any")
                .Single(x => x.GetParameters().Length == 1)
                .MakeGenericMethod(typeof(string));

            var compatibleDeviceParameter = Expression.Parameter(typeof(string), "d");
            var selectInnerExpression =
                Expression.Lambda<Func<string, string>>(compatibleDeviceParameter, compatibleDeviceParameter);

            Expression selectCall = Expression.Call(selectMethod, compatibleDevicesProperty, selectInnerExpression);
            Expression distinctCall = Expression.Call(distinctMethod, selectCall);
            Expression intersectCall = Expression.Call(intersectMethod, distinctCall, deviceIdValues);
            Expression anyCall = Expression.Call(anyMethod, intersectCall);

            baseExpression = Expression.AndAlso(baseExpression, anyCall);
        }
    }
}