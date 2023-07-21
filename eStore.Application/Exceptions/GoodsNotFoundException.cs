using System;
using System.Runtime.Serialization;

namespace eStore.Application.Exceptions;

[Serializable]
public class GoodsNotFoundException : ApplicationException
{
    public GoodsNotFoundException()
    {
    }

    public GoodsNotFoundException(string message) : base(message)
    {
    }

    public GoodsNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public GoodsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}