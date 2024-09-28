using TaxAssistant.Extensions;

namespace TaxAssistant.External.Settings;

public record LLMSettings : IHTTPOptions
{
    public static string SectionName { get; set; } = "LLMSettings";
    public string ModelName { get; init; } = null!;
    public int MaxTokens { get; init; }
    public int Temperature { get; init; }
    public string BaseURL { get; init; }
    public string APIKey { get; init; }
}