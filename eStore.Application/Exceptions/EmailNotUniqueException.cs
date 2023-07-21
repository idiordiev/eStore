using System;
using System.Runtime.Serialization;

namespace eStore.Application.Exceptions;

[Serializable]
public class EmailNotUniqueException : ApplicationException
{
    public EmailNotUniqueException()
    {
    }

    public EmailNotUniqueException(string message) : base(message)
    {
    }

    public EmailNotUniqueException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public EmailNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}