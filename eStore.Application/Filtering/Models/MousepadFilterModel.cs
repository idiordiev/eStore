using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Application.Filtering.Models.Shared;
using eStore.Application.Utility;
using eStore.Domain.Entities;

namespace eStore.Application.Filtering.Models;

public class MousepadFilterModel : GoodsFilterModel
{
    public ICollection<bool> IsStitchedValues { get; set; }
    public ICollection<string> BottomMaterials { get; set; }
    public ICollection<string> TopMaterials { get; set; }
    public ICollection<string> Backlights { get; set; }
        
    public Expression<Func<Mousepad, bool>> GetPredicate()
    {
        var expression = PredicateBuilder.True<Mousepad>().And(m => !m.IsDeleted);

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

        if (Backlights != null && Backlights.Any())
        {
            expression = expression.And(m => Backlights.Contains(m.Backlight));
        }

        if (IsStitchedValues != null && IsStitchedValues.Any())
        {
            expression = expression.And(m => IsStitchedValues.Contains(m.IsStitched));
        }

        if (BottomMaterials != null && BottomMaterials.Any())
        {
            expression = expression.And(m => BottomMaterials.Contains(m.BottomMaterial));
        }

        if (TopMaterials != null && TopMaterials.Any())
        {
            expression = expression.And(m => TopMaterials.Contains(m.TopMaterial));
        }

        return expression;
    }
}