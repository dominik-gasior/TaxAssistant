namespace TaxAssistant.External.Responses;

public record OpenAPIResponse
{
    public required Choice[] Choices { get; init; }
}

public record Choice
{
    public required Message Message { get; init; }
}

public record Message
{
    public required string Role { get; init; }
    public required string Content { get; init; }
}
