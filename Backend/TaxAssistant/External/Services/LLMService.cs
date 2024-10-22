using Microsoft.Extensions.Options;
using TaxAssistant.Extensions;
using TaxAssistant.External.Clients;
using TaxAssistant.External.Responses;
using TaxAssistant.External.Settings;

namespace TaxAssistant.External.Services;

public interface ILLMService
{
    public Task<string> GenerateMessageAsync(string prompt, string type = "json_object");
}

public class LLMService : ILLMService
{
    private readonly LlmClient _llmClient;
    private readonly LLMSettings _llmSettings;

    public LLMService(IOptions<LLMSettings> llmSettings, LlmClient llmClient)
    {
        _llmClient = llmClient;
        _llmSettings = llmSettings.Value;
    }

    public async Task<string> GenerateMessageAsync(string prompt, string type = "json_object")
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
        
        var response = await _llmClient.GetCompletions(requestBody);

        return response!.Choices[0].Message.Content;
    }
}
