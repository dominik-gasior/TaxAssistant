using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Services;
using TaxAssistant.Models;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class UpdateFormController : ControllerBase
{
    private readonly ConversationDumper _conversationDumper;
    private readonly ConversationReader _conversationReader;

    public UpdateFormController(
        ConversationDumper conversationDumper,
        ConversationReader conversationReader)
    {
        _conversationDumper = conversationDumper;
        _conversationReader = conversationReader;
    }

    [HttpPut("update-form/{conversationId}")]
    public async Task<IActionResult> Put(string conversationId, [FromBody] FormModel formModel)
    {
        //TODO: add validation
        var conversation = await _conversationReader.GetLatestConversationLog(conversationId);
        if (conversation is null)
        {
            await _conversationDumper.DumpConversationLog(new ConversationData
            {
                FormModel = formModel, ChatLog = [], Id = conversationId
            });
        }
        else
        {
            await _conversationDumper.DumpConversationLog(conversation with { FormModel = formModel });
        }

        return Ok();
    }
}