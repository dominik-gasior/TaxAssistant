using TaxAssistant.Utils;

namespace TaxAssistant.External.Settings;

public class TerytSettings : IHTTPOptions
{
    public static string SectionName { get; set; } = "TerytSettings";
    public string ModelName { get; init; } = null!;
    public int MaxTokens { get; init; }
    public int Temperature { get; init; }
    public string BaseURL { get; init; }
    public string APIKey { get; init; }
}