using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using TaxAssistant.Clients.Llms;

namespace TaxAssistant.Clients;

public static class Extensions
{
    public static IServiceCollection RegisterClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<LlmSettings>().Bind(config.GetSection(LlmSettings.SectionName));

        services.AddHttpClient<ILlmClient, LlmClient>((sp, c) =>
        {
            var settings = sp.GetRequiredService<IOptions<LlmSettings>>().Value;

            c.BaseAddress = new Uri(settings.BaseUrl);
            c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.ApiKey);
        });

        return services;
    }
}