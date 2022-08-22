using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Factory
{
    public class MouseExpressionFactory : IExpressionFactory
    {
        public Expression CreateFilterExpression(GoodsFilterModel filterModel)
        {
            if (filterModel is not MouseFilterModel filter)
                throw new ArgumentException("");
            
            Expression baseExpression = Expression.Constant(true);
            var mouseParameter = Expression.Parameter(typeof(Mouse), "m");

            Expression isDeletedProperty = Expression.Property(mouseParameter, nameof(Mouse.IsDeleted));
            Expression notDeletedCondition = Expression.IsFalse(isDeletedProperty);
            baseExpression = Expression.AndAlso(baseExpression, notDeletedCondition);

            if (filter.MinPrice != null)
            {
                Expression left = Expression.Property(mouseParameter, "Price");
                Expression right = Expression.Constant(filter.MinPrice);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.MaxPrice != null)
            {
                Expression left = Expression.Property(mouseParameter, "Price");
                Expression right = Expression.Constant(filter.MaxPrice);
                Expression expression = Expression.LessThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
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
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.MinWeight != null)
            {
                Expression left = Expression.Property(mouseParameter, "Weight");
                Expression right = Expression.Constant(filter.MinWeight);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.MaxWeight != null)
            {
                Expression left = Expression.Property(mouseParameter, "Weight");
                Expression right = Expression.Constant(filter.MaxWeight);
                Expression expression = Expression.LessThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            baseExpression = Expression.Lambda(baseExpression, mouseParameter);
            return baseExpression;
        }
    }
}