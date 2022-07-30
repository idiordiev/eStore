using System;
using System.Runtime.Serialization;

namespace eStore.ApplicationCore.Exceptions
{
    [Serializable]
    public class StatusChangingException : ApplicationException
    {
        public StatusChangingException()
        {
        }

        public StatusChangingException(string message) : base(message)
        {
        }

        public StatusChangingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public StatusChangingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}