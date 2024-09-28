namespace TaxAssistant.Extensions.Exceptions;

public class BadRequestException : TaxAssistantException
{
    public BadRequestException(string message) : base(message)
    {
    }
}
