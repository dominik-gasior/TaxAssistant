using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TaxAssistant.Utils.Exceptions;

namespace TaxAssistant.Utils;

public static class HttpClientExtensions
{
    public static async Task<T?> PostAsJsonAsync<T>(this HttpClient client, string requestUri, object content)
    {
        var json = JsonSerializer.Serialize(content);

        var stringContent = new StringContent
        (
            json,
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );

        var response = await client.PostAsync(requestUri, stringContent);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new BadRequestException(error);
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }
}
