using System;

namespace TaxAssistant.Utils.Exceptions;

public class NotFoundException : TaxAssistantException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
