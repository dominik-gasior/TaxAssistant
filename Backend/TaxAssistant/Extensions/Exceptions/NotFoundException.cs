namespace TaxAssistant.Extensions.Exceptions;

public class NotFoundException : TaxAssistantException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
