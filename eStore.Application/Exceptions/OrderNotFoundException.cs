using System;
using System.Runtime.Serialization;

namespace eStore.Application.Exceptions
{
    [Serializable]
    public class OrderNotFoundException : ApplicationException
    {
        public OrderNotFoundException()
        {
        }

        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OrderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}