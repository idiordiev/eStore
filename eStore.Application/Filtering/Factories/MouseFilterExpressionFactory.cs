using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces;
using eStore.Application.Utility;
using eStore.Domain.Entities;

namespace eStore.Application.Filtering.Factories
{
    internal class MouseFilterExpressionFactory : IFilterExpressionFactory<Mouse, MouseFilterModel>
    {
        public Expression<Func<Mouse, bool>> GetExpression(MouseFilterModel filterModel)
        {
            var expression = PredicateBuilder.True<Mouse>().And(m => !m.IsDeleted);

            AddMinPriceConstraint(ref expression, filterModel.MinPrice);
            AddMaxPriceConstraint(ref expression, filterModel.MaxPrice);
            AddManufacturerConstraint(ref expression, filterModel.Manufacturers);
            AddConnectionTypeConstraint(ref expression, filterModel.ConnectionTypes);
            AddMinWeightConstraint(ref expression, filterModel.MinWeight);
            AddMaxWeightConstraint(ref expression, filterModel.MaxWeight);
            AddBacklightConstraint(ref expression, filterModel.Backlights);

            return expression;
        }

        private void AddMinPriceConstraint(ref Expression<Func<Mouse, bool>> expression, decimal? price)
        {
            if (price != null)
            {
                expression = expression.And(m => m.Price >= price);
            }
        }

        private void AddMaxPriceConstraint(ref Expression<Func<Mouse, bool>> expression, decimal? price)
        {
            if (price != null)
            {
                expression = expression.And(m => m.Price <= price);
            }
        }

        private void AddManufacturerConstraint(ref Expression<Func<Mouse, bool>> expression,
            ICollection<string> manufacturers)
        {
            if (manufacturers != null && manufacturers.Any())
            {
                expression = expression.And(m => manufacturers.Contains(m.Manufacturer));
            }
        }

        private void AddMinWeightConstraint(ref Expression<Func<Mouse, bool>> expression, float? weight)
        {
            if (weight != null)
            {
                expression = expression.And(m => m.Weight >= weight);
            }
        }

        private void AddMaxWeightConstraint(ref Expression<Func<Mouse, bool>> expression, float? weight)
        {
            if (weight != null)
            {
                expression = expression.And(m => m.Weight <= weight);
            }
        }

        private void AddConnectionTypeConstraint(ref Expression<Func<Mouse, bool>> expression,
            ICollection<string> connectionTypes)
        {
            if (connectionTypes != null && connectionTypes.Any())
            {
                expression = expression.And(m => connectionTypes.Contains(m.ConnectionType));
            }
        }

        private void AddBacklightConstraint(ref Expression<Func<Mouse, bool>> expression,
            ICollection<string> backlights)
        {
            if (backlights != null && backlights.Any())
            {
                expression = expression.And(m => backlights.Contains(m.Backlight));
            }
        }
    }
}