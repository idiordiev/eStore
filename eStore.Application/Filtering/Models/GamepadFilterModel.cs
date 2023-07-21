using System;
using System.Collections.Generic;
using System.Linq;
using eStore.Application.Filtering.Models.Shared;
using eStore.Application.Utility;
using eStore.Domain.Entities;

namespace eStore.Application.Filtering.Models
{
    public class GamepadFilterModel : GoodsFilterModel
    {
        public ICollection<string> ConnectionTypes { get; set; }
        public ICollection<string> Feedbacks { get; set; }
        public ICollection<string> CompatibleDevices { get; set; }

        public Func<Gamepad, bool> GetPredicate()
        {
            var expression = PredicateBuilder.True<Gamepad>().And(g => !g.IsDeleted);

            if (MinPrice != null)
            {
                expression = expression.And(g => g.Price >= MinPrice);
            }

            if (MaxPrice != null)
            {
                expression = expression.And(g => g.Price <= MaxPrice);
            }

            if (Manufacturers != null && Manufacturers.Any())
            {
                expression = expression.And(g => Manufacturers.Contains(g.Manufacturer));
            }

            if (ConnectionTypes != null && ConnectionTypes.Any())
            {
                expression = expression.And(g => ConnectionTypes.Contains(g.ConnectionType));
            }

            if (Feedbacks != null && Feedbacks.Any())
            {
                expression = expression.And(g => Feedbacks.Contains(g.Feedback));
            }

            if (CompatibleDevices != null && CompatibleDevices.Any())
            {
                expression = expression.And(g =>
                    g.CompatibleDevices.Select(d => d).Distinct().Intersect(CompatibleDevices).Any());
            }

            return expression.Compile();
        }
    }
}