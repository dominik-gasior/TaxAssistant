using Microsoft.Extensions.Options;
using TaxAssistant.External.Settings;
using TaxAssistant.Utils;

namespace TaxAssistant.External.Clients;

public class EDeclarationClient : TaxAssistantClient
{
    private readonly HttpClient _httpClient;
    private readonly EDeclarationSettings _settings;

    public EDeclarationClient(IOptions<IHTTPOptions> options, HttpClient client)
    : base(options, client)
    {
        _httpClient = client;
        _settings = options.Value as EDeclarationSettings ?? throw new ArgumentNullException();
    }

    public override HttpClient CreateClient()
    {
        _httpClient.BaseAddress = new Uri(_settings.BaseURL);

        return _httpClient;
    }
}