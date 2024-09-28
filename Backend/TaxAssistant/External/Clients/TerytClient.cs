using Microsoft.Extensions.Options;
using TaxAssistant.External.Settings;

namespace TaxAssistant.External.Clients;

public class TerytClient
{
    private readonly TerytSettings _settings;
    private readonly HttpClient _httpClient;

    public TerytClient(IOptions<TerytSettings> options, HttpClient httpClient)
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
