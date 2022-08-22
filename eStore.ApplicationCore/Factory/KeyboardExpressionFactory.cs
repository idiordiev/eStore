using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Factory
{
    public class KeyboardExpressionFactory : IExpressionFactory
    {
        public Expression CreateFilterExpression(GoodsFilterModel filterModel)
        {
            if (filterModel is not KeyboardFilterModel filter)
                throw new ArgumentException("");
            
            Expression baseExpression = Expression.Constant(true);
            var keyboardParameter = Expression.Parameter(typeof(Keyboard), "k");

            Expression isDeletedProperty = Expression.Property(keyboardParameter, nameof(Keyboard.IsDeleted));
            Expression notDeletedCondition = Expression.IsFalse(isDeletedProperty);
            baseExpression = Expression.AndAlso(baseExpression, notDeletedCondition);

            if (filter.MinPrice != null)
            {
                Expression left = Expression.Property(keyboardParameter, "Price");
                Expression right = Expression.Constant(filter.MinPrice);
                Expression expression = Expression.GreaterThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
            }

            if (filter.MaxPrice != null)
            {
                Expression left = Expression.Property(keyboardParameter, "Price");
                Expression right = Expression.Constant(filter.MaxPrice);
                Expression expression = Expression.LessThanOrEqual(left, right);
                baseExpression = Expression.AndAlso(baseExpression, expression);
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
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
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
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
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
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
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
                baseExpression = Expression.AndAlso(baseExpression, containsExpression);
            }

            baseExpression = Expression.Lambda(baseExpression, keyboardParameter);
            return baseExpression;
        }
    }
}