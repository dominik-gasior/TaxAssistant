using System.Text.Json.Serialization;

namespace TaxAssistant.Declarations.Strategies.Interfaces;

public interface IDeclarationStrategy
{
    public Dictionary<int, string> QuestionsPool { get; }
    public string Description { get; }
    public string DeclarationType { get; }
    public Task<bool> ClassifyAsync(string utterance);
}

public class DeclarationClassification
{
    [JsonPropertyName("isGoodMatch")]
    public bool IsGoodMatch { get; set; }
}