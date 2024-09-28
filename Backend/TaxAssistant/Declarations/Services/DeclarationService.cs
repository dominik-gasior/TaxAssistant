using System.Xml.Serialization;
using TaxAssistant.Declarations.FakeData;
using TaxAssistant.Declarations.Models;

namespace TaxAssistant.Declarations.Services;

public interface IDeclarationService
{
    Task<DeclarationFileResponse> GetDeclarationByIdAsync(Guid id);
}

public class DeclarationService : IDeclarationService
{
    public string GenerateNextFieldQuestion()
    {
        var nextQuestion = string.Empty;


        return nextQuestion;
    }
    
    public Task<DeclarationFileResponse> GetDeclarationByIdAsync(Guid id)
    {
        //TODO : Get declaration from the store by id
        var declaration = DeclarationsProvider.GetDeclaration();
    
        var writer = new XmlSerializer(declaration.GetType());
        var stream = new MemoryStream();
        
        writer.Serialize(stream, declaration);

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var fileName = $"declaration_{timestamp}.xml";

        return Task.FromResult
        (
            new DeclarationFileResponse
            (
                stream.ToArray(), 
                fileName
            )
        );
    }
}