using TaxAssistant.External.Clients;
using TaxAssistant.External.Llms;
using TaxAssistant.External.Services;
using TaxAssistant.External.Settings;

namespace TaxAssistant.Utils;

public static class Extensions
{
    public static IServiceCollection RegisterClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<LLMSettings>().Bind(config.GetSection(LLMSettings.SectionName));
        services.AddOptions<EDeclarationSettings>().Bind(config.GetSection(EDeclarationSettings.SectionName));
        services.AddOptions<TerytSettings>().Bind(config.GetSection(TerytSettings.SectionName));

        services.AddHttpClient<LLMClient>();
        services.AddHttpClient<EDeclarationClient>();
        services.AddHttpClient<TerytClient>();

        services.AddScoped<ILLMService, LLMService>();

        return services;
    }
}