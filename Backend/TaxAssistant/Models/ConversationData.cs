using System.Text.Json.Serialization;

namespace TaxAssistant.Models;

public record ConversationData
{
    public required string Id { get; init; }
    
    [JsonPropertyName("messages")]
    public required List<Message> ChatLog { get; init; } = [];
    
    [JsonPropertyName("formData")]
    public required FormModel FormModel { get; init; }
}

public record Message
{
    [JsonPropertyName("timestamp")]
    public required long TimeStamp { get; init; }
    public required string Role { get; init; }
    public required string Content { get; init; }
}

public static class Roles
{
    public static string User => "USER";
    public static string Bot => "BOT";
}

