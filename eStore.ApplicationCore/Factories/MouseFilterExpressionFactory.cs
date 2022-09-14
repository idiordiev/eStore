using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;

namespace eStore.ApplicationCore.Factories
{
    internal class MouseFilterExpressionFactory : IFilterExpressionFactory<Mouse>
    {
        public Expression<Func<Mouse, bool>> CreateExpression(GoodsFilterModel filterModel)
        {
            if (filterModel is not MouseFilterModel filter)
                throw new ArgumentException("This factory can only accept the MouseFilterModel.");

            var mouseParameter = Expression.Parameter(typeof(Mouse), "m");
            var filterExpression = GetBaseExpression(mouseParameter);
            AddMinPriceConstraint(ref filterExpression, mouseParameter, filter.MinPrice);
            AddMaxPriceConstraint(ref filterExpression, mouseParameter, filter.MaxPrice);
            AddManufacturerConstraint(ref filterExpression, mouseParameter, filter.ManufacturerIds);
            AddConnectionTypeConstraint(ref filterExpression, mouseParameter, filter.ConnectionTypeIds);
            AddMinWeightConstraint(ref filterExpression, mouseParameter, filter.MinWeight);
            AddMaxWeightConstraint(ref filterExpression, mouseParameter, filter.MaxWeight);
            AddBacklightConstraint(ref filterExpression, mouseParameter, filter.BacklightIds);

            return Expression.Lambda<Func<Mouse, bool>>(filterExpression, mouseParameter);
        }

        private Expression GetBaseExpression(ParameterExpression parameter)
        {
            Expression isDeletedProperty = Expression.Property(parameter, nameof(Mouse.IsDeleted));
            Expression baseExpression = Expression.IsFalse(isDeletedProperty);
            return baseExpression;
        }

        private void AddMinPriceConstraint(ref Expression baseExpression, ParameterExpression parameter, decimal? price)
        {
            if (price == null)
                return;

            Expression left = Expression.Property(parameter, nameof(Mouse.Price));
            Expression right = Expression.Constant(price.Value);
            Expression expression = Expression.GreaterThanOrEqual(left, right);
            baseExpression = Expression.AndAlso(baseExpression, expression);
        }

        private void AddMaxPriceConstraint(ref Expression baseExpression, ParameterExpression parameter, decimal? price)
        {
            if (price == null)
                return;

            Expression left = Expression.Property(parameter, nameof(Mouse.Price));
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
            Expression property = Expression.Property(parameter, nameof(Mouse.ManufacturerId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddMinWeightConstraint(ref Expression baseExpression, ParameterExpression parameter, float? weight)
        {
            if (weight == null)
                return;

            Expression left = Expression.Property(parameter, nameof(Mouse.Weight));
            Expression right = Expression.Constant(weight.Value);
            Expression expression = Expression.GreaterThanOrEqual(left, right);
            baseExpression = Expression.AndAlso(baseExpression, expression);
        }

        private void AddMaxWeightConstraint(ref Expression baseExpression, ParameterExpression parameter, float? weight)
        {
            if (weight == null)
                return;

            Expression left = Expression.Property(parameter, nameof(Mouse.Weight));
            Expression right = Expression.Constant(weight.Value);
            Expression expression = Expression.LessThanOrEqual(left, right);
            baseExpression = Expression.AndAlso(baseExpression, expression);
        }

        private void AddConnectionTypeConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> connectionTypeIds)
        {
            if (connectionTypeIds == null || !connectionTypeIds.Any())
                return;

            Expression values = Expression.Constant(connectionTypeIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Mouse.ConnectionTypeId));
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
            Expression property = Expression.Property(parameter, nameof(Mouse.BacklightId));
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