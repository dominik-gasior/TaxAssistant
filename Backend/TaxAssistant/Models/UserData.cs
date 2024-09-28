namespace TaxAssistant.Models;

public record UserData
{
    public required string Id { get; init; }
    public required ChatLog ChatLog { get; init; }
    public required FormLog FormLog { get; init; }
}

public record ChatLog;
public record FormLog;

