using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using TaxAssistant.External.Llms;
using TaxAssistant.Utils;

namespace TaxAssistant.External.Clients;

public class LLMClient : TaxAssistantClient
{
    private readonly LLMSettings _llmSettings;

    public LLMClient(IOptions<LLMSettings> options, HttpClient client)
    : base(options, client)
    {
        _llmSettings = options.Value ?? throw new ArgumentNullException();
    }

    public override HttpClient CreateClient()
    {
        _httpClient.BaseAddress = new Uri(_llmSettings.BaseURL);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _llmSettings.APIKey);

        return _httpClient;
    }
}