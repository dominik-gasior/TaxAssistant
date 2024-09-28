using System;

namespace TaxAssistant.Utils.Exceptions;

public abstract class TaxAssistantException : Exception
{
    protected string Message { get; set; }

    public TaxAssistantException(string message) : base(message)
    {
        Message = message;
    }
}
