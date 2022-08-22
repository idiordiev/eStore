using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Factory
{
    public class MousepadExpressionFactory : IExpressionFactory
    {
        public Expression CreateFilterExpression(GoodsFilterModel filterModel)
        {
            if (filterModel is not MousepadFilterModel filter)
                throw new ArgumentException("");
            
            Expression baseExpression = Expression.Constant(true);
            var mousepadParameter = Expression.Parameter(typeof(Mousepad), "m");

            Expression isDeletedProperty = Expression.Property(mousepadParameter, nameof(Mousepad.IsDeleted));
            Expression notDeletedCondition = Expression.IsFalse(isDeletedProperty);
            baseExpression = Expression.AndAlso(baseExpression, notDeletedCondition);

            if (filter.MinPrice != null)
            {
                Expression left = Expression.Property(mousepadParameter, "Price");
                Expression right = Expression.Constant(filter.MinPrice);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.MaxPrice != null)
            {
                Expression left = Expression.Property(mousepadParameter, "Price");
                Expression right = Expression.Constant(filter.MaxPrice);
                Expression expression = Expression.LessThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.ManufacturerIds != null && filter.ManufacturerIds.Any())
            {
                Expression values = Expression.Constant(filter.ManufacturerIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(mousepadParameter, "ManufacturerId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.IsStitchedValues != null && filter.IsStitchedValues.Any())
            {
                Expression values = Expression.Constant(filter.IsStitchedValues, typeof(IEnumerable<bool>));
                Expression property = Expression.Property(mousepadParameter, "IsStitched");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(bool));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.BottomMaterialIds != null && filter.BottomMaterialIds.Any())
            {
                Expression values = Expression.Constant(filter.BottomMaterialIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(mousepadParameter, "BottomMaterialId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            if (filter.TopMaterialIds != null && filter.TopMaterialIds.Any())
            {
                Expression values = Expression.Constant(filter.TopMaterialIds, typeof(IEnumerable<int>));
                Expression property = Expression.Property(mousepadParameter, "TopMaterialId");
                var method = typeof(Enumerable)
                    .GetMethods()
                    .Where(x => x.Name == "Contains")
                    .Single(x => x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));
                Expression containsExpression = Expression.Call(method, values, property);
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            baseExpression = Expression.Lambda(baseExpression, mousepadParameter);
            return baseExpression;
        }
    }
}