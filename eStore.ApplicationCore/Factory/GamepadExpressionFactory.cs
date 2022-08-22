using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Factory
{
    public class GamepadExpressionFactory : IExpressionFactory
    {
        public Expression CreateFilterExpression(GoodsFilterModel filterModel)
        {
            if (filterModel is not GamepadFilterModel filter)
                throw new ArgumentException("");
            
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