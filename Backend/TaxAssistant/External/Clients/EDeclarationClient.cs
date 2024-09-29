using System.Net.Http.Headers;
using System.Text;
using TaxAssistant.Extensions.Exceptions;

namespace TaxAssistant.External.Clients;

public class EDeclarationClient
{
    private readonly HttpClient _httpClient;

    public EDeclarationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendForm(string xml)
    {
        var response = await _httpClient.PostAsJsonAsync("upload/declaration", xml);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new BadRequestException(error);
        }
    }
}