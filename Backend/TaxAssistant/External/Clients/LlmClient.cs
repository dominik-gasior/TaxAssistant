using Microsoft.Extensions.Options;
using TaxAssistant.External.Settings;

namespace TaxAssistant.External.Clients;

public class LlmClient
{
    private readonly HttpClient _httpClient;
    private readonly LLMSettings _llmSettings;

    public LlmClient(IOptions<LLMSettings> options, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _llmSettings = options.Value;
    }

}