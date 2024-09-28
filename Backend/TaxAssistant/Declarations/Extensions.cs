using TaxAssistant.Declarations.Services;
using TaxAssistant.Declarations.Strategies;
using TaxAssistant.Declarations.Strategies.Interfaces;

namespace TaxAssistant.Declarations;

public static class Extensions
{
    public static IServiceCollection RegisterDeclarations(this IServiceCollection services)
    {
        services.AddScoped<IDeclarationService, DeclarationService>();
        services.AddScoped<IDeclarationStrategy, PCC3>();

        return services;
    }
}