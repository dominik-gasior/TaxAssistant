using Microsoft.Extensions.Options;
using TaxAssistant.External.Clients;
using TaxAssistant.External.Llms;
using TaxAssistant.External.Responses;
using TaxAssistant.Utils;

namespace TaxAssistant.External.Services;

public interface ILLMService
{
    public Task<OpenAPIResponse?> GenerateMessageAsync(string prompt, string type = "json_object");
}

public class LLMService : ILLMService
{
    private readonly HttpClient client;
    private readonly LLMSettings _llmSettings;

    public LLMService(IOptions<LLMSettings> llmSettings, LLMClient client)
    {
        _llmSettings = llmSettings.Value;
        this.client = client.CreateClient();
    }

    public async Task<OpenAPIResponse?> GenerateMessageAsync(string prompt, string type = "json_object")
    {
        var requestBody = new
        {
            model = _llmSettings.ModelName,
            messages = new[]
            {
                new
                {
                    role = "assistant",
                    content = prompt
                }
            },
            max_tokens = _llmSettings.MaxTokens,
            temperature = _llmSettings.Temperature,
            response_format = new
            {
                type
            }
        };

        var response = await client.PostAsJsonAsync<OpenAPIResponse>("chat/completions", requestBody);

        return response;
    }
}
