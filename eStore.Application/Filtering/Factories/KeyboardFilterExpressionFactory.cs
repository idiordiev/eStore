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
    internal class KeyboardFilterExpressionFactory : IFilterExpressionFactory<Keyboard, KeyboardFilterModel>
    {
        public Expression<Func<Keyboard, bool>> GetExpression(KeyboardFilterModel filterModel)
        {
            var expression = PredicateBuilder.True<Keyboard>().And(k => !k.IsDeleted);

            AddMinPriceConstraint(ref expression, filterModel.MinPrice);
            AddMaxPriceConstraint(ref expression, filterModel.MaxPrice);
            AddManufacturerConstraint(ref expression, filterModel.Manufacturers);
            AddConnectionTypeConstraint(ref expression, filterModel.ConnectionTypes);
            AddSwitchConstraint(ref expression, filterModel.SwitchIds);
            AddKeyboardSizeConstraint(ref expression, filterModel.KeyboardSizes);
            AddKeyboardTypeConstraint(ref expression, filterModel.KeyboardTypes);
            AddKeyRolloverConstraint(ref expression, filterModel.KeyRollovers);
            AddBacklightConstraint(ref expression, filterModel.Backlights);

            return expression;
        }

        private void AddMinPriceConstraint(ref Expression<Func<Keyboard, bool>> expression, decimal? price)
        {
            if (price != null)
            {
                expression = expression.And(k => k.Price >= price);
            }
        }

        private void AddMaxPriceConstraint(ref Expression<Func<Keyboard, bool>> expression, decimal? price)
        {
            if (price != null)
            {
                expression = expression.And(k => k.Price <= price);
            }
        }

        private void AddManufacturerConstraint(ref Expression<Func<Keyboard, bool>> expression,
            ICollection<string> manufacturers)
        {
            if (manufacturers != null && manufacturers.Any())
            {
                expression = expression.And(k => manufacturers.Contains(k.Manufacturer));
            }
        }

        private void AddConnectionTypeConstraint(ref Expression<Func<Keyboard, bool>> expression,
            ICollection<string> connectionTypes)
        {
            if (connectionTypes != null && connectionTypes.Any())
            {
                expression = expression.And(k => connectionTypes.Contains(k.ConnectionType));
            }
        }

        private void AddSwitchConstraint(ref Expression<Func<Keyboard, bool>> expression, ICollection<int?> switchIds)
        {
            if (switchIds != null && switchIds.Any())
            {
                expression = expression.And(k => switchIds.Contains(k.SwitchId));
            }
        }

        private void AddKeyboardSizeConstraint(ref Expression<Func<Keyboard, bool>> expression,
            ICollection<string> sizes)
        {
            if (sizes != null && sizes.Any())
            {
                expression = expression.And(k => sizes.Contains(k.Size));
            }
        }

        private void AddKeyboardTypeConstraint(ref Expression<Func<Keyboard, bool>> expression,
            ICollection<string> types)
        {
            if (types != null && types.Any())
            {
                expression = expression.And(k => types.Contains(k.Type));
            }
        }

        private void AddKeyRolloverConstraint(ref Expression<Func<Keyboard, bool>> expression,
            ICollection<string> rollovers)
        {
            if (rollovers != null && rollovers.Any())
            {
                expression = expression.And(k => rollovers.Contains(k.KeyRollover));
            }
        }

        private void AddBacklightConstraint(ref Expression<Func<Keyboard, bool>> expression,
            ICollection<string> backlights)
        {
            if (backlights != null && backlights.Any())
            {
                expression = expression.And(k => backlights.Contains(k.Backlight));
            }
        }
    }
}