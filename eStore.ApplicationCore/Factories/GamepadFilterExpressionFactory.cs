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
                throw new ArgumentException("");
            
            ParameterExpression gamepadParameter = Expression.Parameter(typeof(Gamepad), "g");
            Expression isDeletedProperty = Expression.Property(gamepadParameter, nameof(Gamepad.IsDeleted));
            Expression filterExpression = Expression.IsFalse(isDeletedProperty);

            if (filter.MinPrice != null)
            {
                Expression left = Expression.Property(gamepadParameter, "Price");
                Expression right = Expression.Constant(filter.MinPrice);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                filterExpression = Expression.AndAlso(filterExpression, expression);
            }

            if (filter.MaxPrice != null)
            {
                Expression left = Expression.Property(gamepadParameter, "Price");
                Expression right = Expression.Constant(filter.MaxPrice);
                Expression expression = Expression.LessThanOrEqual(left, right);
                filterExpression = Expression.AndAlso(filterExpression, expression);
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
                filterExpression = Expression.AndAlso(filterExpression, containsExpression);
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
                filterExpression = Expression.AndAlso(filterExpression, containsExpression);
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
                filterExpression = Expression.AndAlso(filterExpression, containsExpression);
            }

            Expression<Func<Gamepad, bool>> result = (Expression<Func<Gamepad, bool>>)Expression.Lambda(filterExpression, gamepadParameter);
            return result;
        }
    }
}