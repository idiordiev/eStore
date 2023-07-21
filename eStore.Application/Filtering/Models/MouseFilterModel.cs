using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Application.Filtering.Models.Shared;
using eStore.Application.Utility;
using eStore.Domain.Entities;

namespace eStore.Application.Filtering.Models;

public class MouseFilterModel : GoodsFilterModel
{
    public float? MinWeight { get; set; }
    public float? MaxWeight { get; set; }
    public ICollection<string> ConnectionTypes { get; set; }
    public ICollection<string> Backlights { get; set; }

    public Expression<Func<Mouse, bool>> GetPredicate()
    {
        var expression = PredicateBuilder.True<Mouse>().And(m => !m.IsDeleted);

        if (MinPrice != null)
        {
            expression = expression.And(m => m.Price >= MinPrice);
        }

        if (MaxPrice != null)
        {
            expression = expression.And(m => m.Price <= MaxPrice);
        }

        if (Manufacturers != null && Manufacturers.Any())
        {
            expression = expression.And(m => Manufacturers.Contains(m.Manufacturer));
        }

        if (ConnectionTypes != null && ConnectionTypes.Any())
        {
            expression = expression.And(m => ConnectionTypes.Contains(m.ConnectionType));
        }

        if (MinWeight != null)
        {
            expression = expression.And(m => m.Weight >= MinWeight);
        }

        if (MaxWeight != null)
        {
            expression = expression.And(m => m.Weight <= MaxWeight);
        }

        if (Backlights != null && Backlights.Any())
        {
            expression = expression.And(m => Backlights.Contains(m.Backlight));
        }

        return expression;
    }
}