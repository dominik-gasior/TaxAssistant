using TaxAssistant.External.Clients;
using TaxAssistant.External.Llms;

namespace TaxAssistant.Utils;

public static class Extensions
{
    public static IServiceCollection RegisterClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<LLMSettings>().Bind(config.GetSection(LLMSettings.SectionName));

        services.AddHttpClient<LLMClient>();

        return services;
    }
}