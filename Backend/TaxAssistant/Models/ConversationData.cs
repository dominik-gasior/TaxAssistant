namespace TaxAssistant.Models;

public record ConversationData
{
    public required string Id { get; init; }
    public required List<Message> ChatLog { get; init; } = [];
    public required FormModel FormModel { get; init; }
}

public record Message
{
    public int TimeStamp { get; init; }
    public string Role { get; init; }
    public string Content { get; init; }
}

