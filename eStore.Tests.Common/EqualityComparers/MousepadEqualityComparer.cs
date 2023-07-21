using System;
using System.Collections.Generic;
using eStore.Domain.Entities;

namespace eStore.Tests.Common.EqualityComparers;

public class MousepadEqualityComparer : IEqualityComparer<Mousepad>
{
    public bool Equals(Mousepad x, Mousepad y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.Id == y.Id
               && x.IsDeleted == y.IsDeleted
               && x.IsStitched == y.IsStitched
               && x.TopMaterial == y.TopMaterial
               && x.BottomMaterial == y.BottomMaterial
               && x.Backlight == y.Backlight
               && x.Length.Equals(y.Length)
               && x.Width.Equals(y.Width)
               && x.Height.Equals(y.Height);
    }

    public int GetHashCode(Mousepad obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Id);
        hashCode.Add(obj.IsDeleted);
        hashCode.Add(obj.IsStitched);
        hashCode.Add(obj.TopMaterial);
        hashCode.Add(obj.BottomMaterial);
        hashCode.Add(obj.Backlight);
        hashCode.Add(obj.Length);
        hashCode.Add(obj.Width);
        hashCode.Add(obj.Height);

        return hashCode.ToHashCode();
    }
}