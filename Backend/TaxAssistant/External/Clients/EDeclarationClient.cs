using Microsoft.Extensions.Options;
using TaxAssistant.External.Settings;

namespace TaxAssistant.External.Clients;

public class EDeclarationClient
{
    private readonly EDeclarationSettings _settings;
    private readonly HttpClient _httpClient;

    public EDeclarationClient(IOptions<EDeclarationSettings> options, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _settings = options.Value ?? throw new ArgumentNullException();
    }

    public HttpClient CreateClient()
    {
        _httpClient.BaseAddress = new Uri(_settings.BaseURL);

        return _httpClient;
    }
}