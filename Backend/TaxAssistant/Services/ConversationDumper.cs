using System.Text.Json;
using TaxAssistant.Models;

namespace TaxAssistant.Services;

public class ConversationDumper
{
    public async Task DumpConversationLog(ConversationData conversationData) => await File.WriteAllTextAsync(
        $"/dumps/{conversationData.Id}",
        JsonSerializer.Serialize(conversationData));
}

public class ConversationReader
{
    public async Task<ConversationData?>  GetLatestConversationLog(string conversationId)
    {
        if (!File.Exists($"/dumps/{conversationId}")) return null;
        var data = await File.ReadAllTextAsync($"/dumps/{conversationId}");
        return JsonSerializer.Deserialize<ConversationData>(data);
    }
}

