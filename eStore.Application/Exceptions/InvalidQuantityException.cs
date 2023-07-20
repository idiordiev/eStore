using System;
using System.Runtime.Serialization;

namespace eStore.Application.Exceptions
{
    [Serializable]
    public class InvalidQuantityException : ApplicationException
    {
        public InvalidQuantityException()
        {
        }

        public InvalidQuantityException(string message) : base(message)
        {
        }

        public InvalidQuantityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidQuantityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}