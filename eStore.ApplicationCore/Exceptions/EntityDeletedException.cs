using System;
using System.Runtime.Serialization;

namespace eStore.ApplicationCore.Exceptions
{
    [Serializable]
    public class EntityDeletedException : ApplicationException
    {
        public EntityDeletedException()
        {
        }

        public EntityDeletedException(string message) : base(message)
        {
        }

        public EntityDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EntityDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}