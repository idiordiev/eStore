using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;

namespace eStore.ApplicationCore.Factories
{
    internal class MousepadFilterExpressionFactory : IFilterExpressionFactory<Mousepad>
    {
        public Expression<Func<Mousepad, bool>> CreateExpression(GoodsFilterModel filterModel)
        {
            if (filterModel is not MousepadFilterModel filter)
            {
                throw new ArgumentException("This factory can only accept the MousepadFilterModel.");
            }

            var mousepadParameter = Expression.Parameter(typeof(Mousepad), "m");
            var filterExpression = GetBaseExpression(mousepadParameter);
            AddMinPriceConstraint(ref filterExpression, mousepadParameter, filter.MinPrice);
            AddMaxPriceConstraint(ref filterExpression, mousepadParameter, filter.MaxPrice);
            AddManufacturerConstraint(ref filterExpression, mousepadParameter, filter.ManufacturerIds);
            AddBacklightConstraint(ref filterExpression, mousepadParameter, filter.BacklightIds);
            AddIsStitchedConstraint(ref filterExpression, mousepadParameter, filter.IsStitchedValues);
            AddBottomMaterialConstraint(ref filterExpression, mousepadParameter, filter.BottomMaterialIds);
            AddTopMaterialConstraint(ref filterExpression, mousepadParameter, filter.TopMaterialIds);

            return Expression.Lambda<Func<Mousepad, bool>>(filterExpression, mousepadParameter);
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
            {
                return;
            }

            Expression left = Expression.Property(parameter, nameof(Mousepad.Price));
            Expression right = Expression.Constant(price.Value);
            Expression expression = Expression.GreaterThanOrEqual(left, right);
            baseExpression = Expression.AndAlso(baseExpression, expression);
        }

        private void AddMaxPriceConstraint(ref Expression baseExpression, ParameterExpression parameter, decimal? price)
        {
            if (price == null)
            {
                return;
            }

            Expression left = Expression.Property(parameter, nameof(Mousepad.Price));
            Expression right = Expression.Constant(price.Value);
            Expression expression = Expression.LessThanOrEqual(left, right);
            baseExpression = Expression.AndAlso(baseExpression, expression);
        }

        private void AddManufacturerConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> manufacturerIds)
        {
            if (manufacturerIds == null || !manufacturerIds.Any())
            {
                return;
            }

            Expression values = Expression.Constant(manufacturerIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Mousepad.ManufacturerId));
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
            {
                return;
            }

            Expression values = Expression.Constant(backlightIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Mousepad.BacklightId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddIsStitchedConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<bool> isStitchedValues)
        {
            if (isStitchedValues == null || !isStitchedValues.Any())
            {
                return;
            }

            Expression values = Expression.Constant(isStitchedValues, typeof(IEnumerable<bool>));
            Expression property = Expression.Property(parameter, nameof(Mousepad.IsStitched));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(bool));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddBottomMaterialConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> materialIds)
        {
            if (materialIds == null || !materialIds.Any())
            {
                return;
            }

            Expression values = Expression.Constant(materialIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Mousepad.BottomMaterialId));
            var method = typeof(Enumerable)
                .GetMethods()
                .Where(x => x.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(int));
            Expression containsExpression = Expression.Call(method, values, property);
            baseExpression = Expression.AndAlso(baseExpression, containsExpression);
        }

        private void AddTopMaterialConstraint(ref Expression baseExpression, ParameterExpression parameter,
            IEnumerable<int> materialIds)
        {
            if (materialIds == null || !materialIds.Any())
            {
                return;
            }

            Expression values = Expression.Constant(materialIds, typeof(IEnumerable<int>));
            Expression property = Expression.Property(parameter, nameof(Mousepad.TopMaterialId));
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