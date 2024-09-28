using TaxAssistant.Extensions;

namespace TaxAssistant.External.Settings;

public class TerytSettings : IHTTPOptions
{
    public static string SectionName { get; set; } = "TerytSettings";
    public string BaseURL { get; init; }
    public string APIKey { get; init; }
}