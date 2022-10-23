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
    internal class MousepadFilterExpressionFactory : IFilterExpressionFactory<Mousepad, MousepadFilterModel>
    {
        public Expression<Func<Mousepad, bool>> GetExpression(MousepadFilterModel filterModel)
        {
            var expression = PredicateBuilder.True<Mousepad>().And(m => !m.IsDeleted);

            AddMinPriceConstraint(ref expression, filterModel.MinPrice);
            AddMaxPriceConstraint(ref expression, filterModel.MaxPrice);
            AddManufacturerConstraint(ref expression, filterModel.Manufacturers);
            AddBacklightConstraint(ref expression, filterModel.Backlights);
            AddIsStitchedConstraint(ref expression, filterModel.IsStitchedValues);
            AddBottomMaterialConstraint(ref expression, filterModel.BottomMaterials);
            AddTopMaterialConstraint(ref expression, filterModel.TopMaterials);

            return expression;
        }

        private void AddMinPriceConstraint(ref Expression<Func<Mousepad, bool>> expression, decimal? price)
        {
            if (price != null)
                expression = expression.And(m => m.Price >= price);
        }

        private void AddMaxPriceConstraint(ref Expression<Func<Mousepad, bool>> expression, decimal? price)
        {
            if (price != null)
                expression = expression.And(m => m.Price <= price);
        }

        private void AddManufacturerConstraint(ref Expression<Func<Mousepad, bool>> expression, ICollection<string> manufacturers)
        {
            if (manufacturers != null && manufacturers.Any())
                expression = expression.And(m => manufacturers.Contains(m.Manufacturer));
        }

        private void AddBacklightConstraint(ref Expression<Func<Mousepad, bool>> expression, ICollection<string> backlights)
        {
            if (backlights != null && backlights.Any())
                expression = expression.And(m => backlights.Contains(m.Backlight));
        }

        private void AddIsStitchedConstraint(ref Expression<Func<Mousepad, bool>> expression, ICollection<bool> isStitchedValues)
        {
            if (isStitchedValues != null && isStitchedValues.Any())
                expression = expression.And(m => isStitchedValues.Contains(m.IsStitched));
        }

        private void AddBottomMaterialConstraint(ref Expression<Func<Mousepad, bool>> expression, ICollection<string> materials)
        {
            if (materials != null && materials.Any())
                expression = expression.And(m => materials.Contains(m.BottomMaterial));
        }

        private void AddTopMaterialConstraint(ref Expression<Func<Mousepad, bool>> expression, ICollection<string> materials)
        {
            if (materials != null && materials.Any())
                expression = expression.And(m => materials.Contains(m.TopMaterial));
        }
    }
}