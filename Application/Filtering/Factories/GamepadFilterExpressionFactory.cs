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
    internal class GamepadFilterExpressionFactory : IFilterExpressionFactory<Gamepad, GamepadFilterModel>
    {
        public Expression<Func<Gamepad, bool>> GetExpression(GamepadFilterModel filterModel)
        {
            var expression = PredicateBuilder.True<Gamepad>().And(g => !g.IsDeleted);

            AddMinPriceConstraint(ref expression, filterModel.MinPrice);
            AddMaxPriceConstraint(ref expression, filterModel.MaxPrice);
            AddManufacturerConstraint(ref expression, filterModel.Manufacturers);
            AddConnectionTypeConstraint(ref expression, filterModel.ConnectionTypes);
            AddFeedbackConstraint(ref expression, filterModel.Feedbacks);
            AddCompatibleDevicesConstraint(ref expression, filterModel.CompatibleDevices);

            return expression;
        }

        private void AddMinPriceConstraint(ref Expression<Func<Gamepad, bool>> expression, decimal? price)
        {
            if (price != null)
                expression = expression.And(g => g.Price >= price);
        }

        private void AddMaxPriceConstraint(ref Expression<Func<Gamepad, bool>> expression, decimal? price)
        {
            if (price != null)
                expression = expression.And(g => g.Price <= price);
        }

        private void AddManufacturerConstraint(ref Expression<Func<Gamepad, bool>> expression, ICollection<string> manufacturers)
        {
            if (manufacturers != null && manufacturers.Any())
                expression = expression.And(g => manufacturers.Any(m => m.Contains(g.Manufacturer)));
        }

        private void AddConnectionTypeConstraint(ref Expression<Func<Gamepad, bool>> expression, ICollection<string> connectionTypes)
        {
            if (connectionTypes != null && connectionTypes.Any())
                expression = expression.And(g => connectionTypes.Contains(g.ConnectionType));
        }

        private void AddFeedbackConstraint(ref Expression<Func<Gamepad, bool>> expression, ICollection<string> feedbacks)
        {
            if (feedbacks != null && feedbacks.Any())
                expression = expression.And(g => feedbacks.Contains(g.Feedback));
        }

        private void AddCompatibleDevicesConstraint(ref Expression<Func<Gamepad, bool>> expression, ICollection<string> devices)
        {
            if (devices != null && devices.Any())
                expression = expression.And(g => g.CompatibleDevices.Select(d => d).Distinct().Intersect(devices).Any());
        }
    }
}