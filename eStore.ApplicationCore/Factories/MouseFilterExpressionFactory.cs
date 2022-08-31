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
                throw new ArgumentException("");
            
            ParameterExpression mouseParameter = Expression.Parameter(typeof(Mouse), "m");
            Expression isDeletedProperty = Expression.Property(mouseParameter, nameof(Mouse.IsDeleted));
            Expression filterExpression = Expression.IsFalse(isDeletedProperty);

            if (filter.MinPrice != null)
            {
                Expression left = Expression.Property(mouseParameter, "Price");
                Expression right = Expression.Constant(filter.MinPrice);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                filterExpression = Expression.AndAlso(filterExpression, expression);
            }

            if (filter.MaxPrice != null)
            {
                Expression left = Expression.Property(mouseParameter, "Price");
                Expression right = Expression.Constant(filter.MaxPrice);
                Expression expression = Expression.LessThanOrEqual(left, right);
                filterExpression = Expression.AndAlso(filterExpression, expression);
            }

            if (filter.ManufacturerIds != null && filter.ManufacturerIds.Any())
            {
                Expression values = Expression.Constant(filter.ManufacturerIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(mouseParameter, "ManufacturerId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                filterExpression = Expression.AndAlso(filterExpression, containsExpression);
            }

            if (filter.MinWeight != null)
            {
                Expression left = Expression.Property(mouseParameter, "Weight");
                Expression right = Expression.Constant(filter.MinWeight);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                filterExpression = Expression.AndAlso(filterExpression, expression);
            }

            if (filter.MaxWeight != null)
            {
                Expression left = Expression.Property(mouseParameter, "Weight");
                Expression right = Expression.Constant(filter.MaxWeight);
                Expression expression = Expression.LessThanOrEqual(left, right);
                filterExpression = Expression.AndAlso(filterExpression, expression);
            }

            Expression<Func<Mouse, bool>> result = (Expression<Func<Mouse, bool>>)Expression.Lambda(filterExpression, mouseParameter);
            return result;
        }
    }
}