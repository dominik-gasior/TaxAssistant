using Microsoft.Extensions.Options;
using TaxAssistant.Utils;

namespace TaxAssistant.External.Clients;

public abstract class TaxAssistantClient
{
    protected readonly HttpClient _httpClient;
    protected readonly IHTTPOptions options;

    public TaxAssistantClient(IOptions<IHTTPOptions> options, HttpClient client)
    {
        _httpClient = client;
        this.options = options.Value;
    }

    public abstract HttpClient CreateClient();
}
