namespace TaxAssistant.Models;

public record ConversationData
{
    public required string Id { get; init; }
    public required List<Message> ChatLog { get; init; } = [];
    public required FormModel FormModel { get; init; }
}

public record Message
{
    public required int TimeStamp { get; init; }
    public required string Role { get; init; }
    public required string Content { get; init; }
}

