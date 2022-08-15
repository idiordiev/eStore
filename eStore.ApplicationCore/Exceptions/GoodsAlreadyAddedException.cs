using System;
using System.Runtime.Serialization;

namespace eStore.ApplicationCore.Exceptions
{
    [Serializable]
    public class GoodsAlreadyAddedException : ApplicationException
    {
        public GoodsAlreadyAddedException()
        {
        }

        public GoodsAlreadyAddedException(string message) : base(message)
        {
        }

        public GoodsAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public GoodsAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}