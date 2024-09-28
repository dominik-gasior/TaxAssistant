namespace TaxAssistant.Clients.Llms;

public sealed class LlmSettings
{
    public static string SectionName => "Llm";

    public string BaseUrl { get; init; } = null!;
    public string ApiKey { get; init; } = null!;
    public string ModelName { get; init; } = null!;
    public int MaxTokens { get; init; }
    public int Temperature { get; init; }
}