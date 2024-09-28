using System.Net.Mime;
using System.Text;
using System.Text.Json;

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

        var response = await client.PostAsJsonAsync(requestUri, stringContent);

        if (!response.IsSuccessStatusCode)
        {
            return default(T?);
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }
}
