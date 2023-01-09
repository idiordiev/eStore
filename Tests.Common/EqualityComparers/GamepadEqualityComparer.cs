using System;
using System.Collections.Generic;
using eStore.Domain.Entities;

namespace eStore.Tests.Common.EqualityComparers
{
    public class GamepadEqualityComparer : IEqualityComparer<Gamepad>
    {
        public bool Equals(Gamepad x, Gamepad y)
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
                   && x.Weight.Equals(y.Weight)
                   && x.ConnectionType == y.ConnectionType
                   && x.Feedback == y.Feedback
                   && Equals(x.CompatibleDevices, y.CompatibleDevices);
        }

        public int GetHashCode(Gamepad obj)
        {
            return HashCode.Combine(obj.Id, obj.IsDeleted, obj.Weight, obj.ConnectionType, obj.Feedback,
                obj.CompatibleDevices);
        }
    }
}