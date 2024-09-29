using System.Text.Json;
using TaxAssistant.Extensions;
using TaxAssistant.Models;

namespace TaxAssistant.Services;

public class ConversationDumper
{
    public async Task DumpConversationLog(ConversationData conversationData)
    {
        var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(outputDirectory, "dumps");
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        filePath = Path.Combine(outputDirectory, $"dumps/{conversationData.Id}.json");
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(conversationData));
    }
}

public class ConversationReader
{
    public async Task<ConversationData?>  GetLatestConversationLog(string conversationId)
    {
        var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(outputDirectory, $"dumps/{conversationId}.json");
        
        if (!File.Exists(filePath)) return null;
        var data = FileExtensions.GetTextFromFile(filePath);
        return data is null ? null : JsonSerializer.Deserialize<ConversationData>(data);
    }
}

