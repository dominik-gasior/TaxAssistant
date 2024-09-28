using TaxAssistant.Utils;

namespace TaxAssistant.External.Settings;

public record EDeclarationSettings : IHTTPOptions
{
    public static string SectionName { get; set; } = "EDeclarationSettings";
    public string BaseURL { get; init; }
    public string APIKey { get; init; }
}