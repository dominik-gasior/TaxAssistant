using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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
        Console.WriteLine($"Rozpoczecie pobierania konwersacji o ID [{conversationId}]");
        
        var conversation = await _conversationReader.GetLatestConversationLog(conversationId);
        var updatedModel = FormModelValidator.UpdateFormModel(conversation?.FormModel ?? new FormModel(), formModel);
        
        if (conversation is null)
        {
            Console.WriteLine($"Nie znaleziono konwersacji o ID [{conversationId}]");
            await _conversationDumper.DumpConversationLog(new ConversationData
            {
                FormModel = updatedModel, ChatLog = [], Id = conversationId
            });
        }
        else
        {
            await _conversationDumper.DumpConversationLog(conversation with { FormModel = updatedModel });
            var logData = JsonSerializer.Serialize(conversation);
            Console.WriteLine($"Zapisano formularz do pliku [{conversationId}] [{logData}]");
        }

        return Ok();
    }
}