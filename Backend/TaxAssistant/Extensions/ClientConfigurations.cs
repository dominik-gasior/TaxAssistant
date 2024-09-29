using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using TaxAssistant.External.Clients;
using TaxAssistant.External.Settings;

namespace TaxAssistant.Extensions;

public static class ClientConfigurations
{
    public static void ConfigureTerytClient(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<TerytSettings>(config.GetSection(TerytSettings.SectionName));
        services.AddHttpClient<TerytClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<TerytSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseURL);
        });
    }

    public static void ConfigureEDeclarationClient(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EDeclarationSettings>(config.GetSection(EDeclarationSettings.SectionName));
        services.AddHttpClient<EDeclarationClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<EDeclarationSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseURL);
        });
    }

    public static void ConfigureLlmClient(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<LLMSettings>(config.GetSection(LLMSettings.SectionName));
        services.AddHttpClient<LlmClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<LLMSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseURL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.APIKey);
        });
    }
}