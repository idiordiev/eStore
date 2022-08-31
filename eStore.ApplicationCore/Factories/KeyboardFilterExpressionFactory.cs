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
                throw new ArgumentException("");
            
            ParameterExpression keyboardParameter = Expression.Parameter(typeof(Keyboard), "k");
            Expression isDeletedProperty = Expression.Property(keyboardParameter, nameof(Keyboard.IsDeleted));
            Expression filterExpression = Expression.IsFalse(isDeletedProperty);

            if (filter.MinPrice != null)
            {
                Expression left = Expression.Property(keyboardParameter, "Price");
                Expression right = Expression.Constant(filter.MinPrice);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                filterExpression = Expression.AndAlso(filterExpression, expression);
            }

            if (filter.MaxPrice != null)
            {
                Expression left = Expression.Property(keyboardParameter, "Price");
                Expression right = Expression.Constant(filter.MaxPrice);
                Expression expression = Expression.LessThanOrEqual(left, right);
                filterExpression = Expression.AndAlso(filterExpression, expression);
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
                filterExpression = Expression.AndAlso(filterExpression, containsExpression);
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
                filterExpression = Expression.AndAlso(filterExpression, containsExpression);
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
                filterExpression = Expression.AndAlso(filterExpression, containsExpression);
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
                filterExpression = Expression.AndAlso(filterExpression, containsExpression);
            }

            Expression<Func<Keyboard, bool>> result = (Expression<Func<Keyboard, bool>>)Expression.Lambda(filterExpression, keyboardParameter);
            return result;
        }
    }
}