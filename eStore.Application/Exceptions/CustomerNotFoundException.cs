using System;
using System.Runtime.Serialization;

namespace eStore.Application.Exceptions;

[Serializable]
public class CustomerNotFoundException : ApplicationException
{
    public CustomerNotFoundException()
    {
    }

    public CustomerNotFoundException(string message) : base(message)
    {
    }

    public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public CustomerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}