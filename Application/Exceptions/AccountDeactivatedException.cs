using System;
using System.Runtime.Serialization;

namespace eStore.Application.Exceptions
{
    [Serializable]
    public class AccountDeactivatedException : ApplicationException
    {
        public AccountDeactivatedException()
        {
        }

        public AccountDeactivatedException(string message) : base(message)
        {
        }

        public AccountDeactivatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AccountDeactivatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}