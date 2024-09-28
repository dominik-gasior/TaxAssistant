using Microsoft.Extensions.Options;
using TaxAssistant.Extensions.Exceptions;
using TaxAssistant.External.Responses;
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

    public async Task<OpenAPIResponse> GetCompletions(object data)
    {
        var response = await _httpClient.PostAsJsonAsync("chat/completions", data);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new BadRequestException(error);
        }

        return await response.Content.ReadFromJsonAsync<OpenAPIResponse>() ?? throw new BadRequestException("TODO");
    }

}