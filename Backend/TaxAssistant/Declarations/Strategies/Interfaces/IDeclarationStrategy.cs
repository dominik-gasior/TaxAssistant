using System.Text.Json.Serialization;

namespace TaxAssistant.Declarations.Strategies.Interfaces;

public interface IDeclarationStrategy
{
    public string Description { get; }
    public string DeclarationType { get; }
    public Task<bool> ClassifyAsync(string utterance);
}

public class DeclarationClassification
{
    [JsonPropertyName("isGoodMatch")]
    public bool IsGoodMatch { get; set; }
}