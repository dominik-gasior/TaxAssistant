using TaxAssistant.External.Clients;

namespace TaxAssistant.External.Services;

public interface IEDeclarationService
{
    public Task SendDeclarationAsync(string xml);
}

public class EDeclarationService : IEDeclarationService
{
    private readonly EDeclarationClient _eDeclarationClient;

    public EDeclarationService(EDeclarationClient eDeclarationClient)
    {
        _eDeclarationClient = eDeclarationClient;
    }

    public async Task SendDeclarationAsync(string xml)
        => await _eDeclarationClient.SendForm(xml);
}
