using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class ChatController : ControllerBase
{
    private readonly ConversationReader _conversationReader;

    public ChatController(ConversationReader conversationReader)
    {
        _conversationReader = conversationReader;
    }

    [HttpGet("restore-chat/{conversationId}")]
    public async Task<IActionResult> Get(string conversationId)
    {
        Console.WriteLine($"Rozpoczecie proby wczytania konwersacji o ID [{conversationId}]");
        var conversation = await _conversationReader.GetLatestConversationLog(conversationId);

        if (conversation is not null)
        {
            var logData = JsonSerializer.Serialize(conversation);
            Console.WriteLine(logData);
            Console.WriteLine($"Znaleziono dane konwersacji o ID [{conversationId}], [{conversation}]");
            
            return Ok(conversation);
        }
        
        Console.WriteLine($"Nie znaleziono konwersacji o ID [{conversationId}]");

        return NotFound();
    }
}