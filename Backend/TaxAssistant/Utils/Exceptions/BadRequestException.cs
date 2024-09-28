using System;

namespace TaxAssistant.Utils.Exceptions;

public class BadRequestException : TaxAssistantException
{
    public BadRequestException(string message) : base(message)
    {
    }
}
