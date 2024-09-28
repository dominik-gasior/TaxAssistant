using Microsoft.Extensions.Options;
using TaxAssistant.External.Settings;

namespace TaxAssistant.External.Clients;

public class TerytClient : TaxAssistantClient
{
    private readonly TerytSettings _settings;

    public TerytClient(IOptions<TerytSettings> options, HttpClient client)
    : base(options, client)
    {
        _settings = options.Value ?? throw new ArgumentNullException();
    }

    public override HttpClient CreateClient()
    {
        _httpClient.BaseAddress = new Uri(_settings.BaseURL);

        return _httpClient;
    }
}
