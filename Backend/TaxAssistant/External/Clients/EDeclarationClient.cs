using Microsoft.Extensions.Options;
using TaxAssistant.External.Settings;

namespace TaxAssistant.External.Clients;

public class EDeclarationClient : TaxAssistantClient
{
    private readonly EDeclarationSettings _settings;

    public EDeclarationClient(IOptions<EDeclarationSettings> options, HttpClient client)
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