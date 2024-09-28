using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace TaxAssistant.Clients.Llms;

public interface ILlmClient
{
    public Task<string> GenerateMessageAsync(string prompt, string type = "json_object");
}

internal sealed class LlmClient : ILlmClient
{
    private readonly HttpClient _httpClient;
    private readonly LlmSettings _llmSettings;

    public LlmClient(IOptions<LlmSettings> llmSettings, HttpClient client)
    {
        _httpClient = client;
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

        var content = new StringContent
        (
            JsonConvert.SerializeObject(requestBody), 
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );
        
        var response = await _httpClient.PostAsync(requestUri: "chat/completions", content);
      
        response.EnsureSuccessStatusCode();
        
        var body = await response.Content.ReadFromJsonAsync<OpenAiResponse>();
        
        return body!.Choices[0].Message.Content;
    }

    public class OpenAiResponse
    {
        [JsonProperty("choices")]
        public required Choice[] Choices { get; set; }
    }

    public class Choice
    {
        [JsonProperty("message")]
        public required Message Message { get; set; }
    }

    public class Message
    {
        public required string Role { get; set; }
        public required string Content { get; set; }
    }
}