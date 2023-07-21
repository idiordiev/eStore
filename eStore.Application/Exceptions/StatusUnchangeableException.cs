using System;
using System.Runtime.Serialization;

namespace eStore.Application.Exceptions;

[Serializable]
public class StatusUnchangeableException : ApplicationException
{
    public StatusUnchangeableException()
    {
    }

    public StatusUnchangeableException(string message) : base(message)
    {
    }

    public StatusUnchangeableException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public StatusUnchangeableException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}