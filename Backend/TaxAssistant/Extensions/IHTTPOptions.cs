namespace TaxAssistant.Extensions;

public interface IHTTPOptions
{
    public static string SectionName { get; set; }
    public string BaseURL { get; init; }
    public string APIKey { get; init; }
}