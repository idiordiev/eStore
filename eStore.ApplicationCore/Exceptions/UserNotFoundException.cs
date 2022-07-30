using System;
using System.Runtime.Serialization;

namespace eStore.ApplicationCore.Exceptions
{
    [Serializable]
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}