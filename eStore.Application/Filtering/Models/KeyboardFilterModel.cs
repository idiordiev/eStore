using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Application.Filtering.Models.Shared;
using eStore.Application.Utility;
using eStore.Domain.Entities;

namespace eStore.Application.Filtering.Models;

public class KeyboardFilterModel : GoodsFilterModel
{
    public ICollection<string> KeyboardTypes { get; set; }
    public ICollection<string> KeyboardSizes { get; set; }
    public ICollection<string> ConnectionTypes { get; set; }
    public ICollection<int?> SwitchIds { get; set; }
    public ICollection<string> KeyRollovers { get; set; }
    public ICollection<string> Backlights { get; set; }

    public Expression<Func<Keyboard, bool>> GetPredicate()
    {
        var expression = PredicateBuilder.True<Keyboard>().And(k => !k.IsDeleted);

        if (MinPrice != null)
        {
            expression = expression.And(k => k.Price >= MinPrice);
        }

        if (MaxPrice != null)
        {
            expression = expression.And(k => k.Price <= MaxPrice);
        }

        if (Manufacturers != null && Manufacturers.Any())
        {
            expression = expression.And(k => Manufacturers.Contains(k.Manufacturer));
        }

        if (ConnectionTypes != null && ConnectionTypes.Any())
        {
            expression = expression.And(k => ConnectionTypes.Contains(k.ConnectionType));
        }

        if (SwitchIds != null && SwitchIds.Any())
        {
            expression = expression.And(k => SwitchIds.Contains(k.SwitchId));
        }

        if (KeyboardSizes != null && KeyboardSizes.Any())
        {
            expression = expression.And(k => KeyboardSizes.Contains(k.Size));
        }

        if (KeyboardTypes != null && KeyboardTypes.Any())
        {
            expression = expression.And(k => KeyboardTypes.Contains(k.Type));
        }

        if (KeyRollovers != null && KeyRollovers.Any())
        {
            expression = expression.And(k => KeyRollovers.Contains(k.KeyRollover));
        }

        if (Backlights != null && Backlights.Any())
        {
            expression = expression.And(k => Backlights.Contains(k.Backlight));
        }

        return expression;
    }
}