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

    [HttpGet("restore-chat")]
    public async Task<IActionResult> Get(string conversationId)
    {
        var conversation = await _conversationReader.GetLatestConversationLog(conversationId);
        return conversation is not null ? Ok(conversation) : NotFound();
    }
}