using Microsoft.AspNetCore.Mvc;
using TaxAssistant.External.Clients;
using TaxAssistant.Models;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class SendFormController : ControllerBase
{
    private readonly IFormService _formService;
    private readonly EDeclarationClient _eDeclarationClient;
    private readonly ConversationReader _conversationReader;

    public SendFormController(
        IFormService formService,
        EDeclarationClient eDeclarationClient,
        ConversationReader conversationReader)
    {
        _formService = formService;
        _eDeclarationClient = eDeclarationClient;
        _conversationReader = conversationReader;
    }

    [HttpPost("send-form/{conversationId}")]
    public async Task<IActionResult> Post(string conversationId)
    {
        Console.WriteLine($"Rozpoczecie pobierania konwersacji o ID: [{conversationId}]");
        
        var conversation = await _conversationReader.GetLatestConversationLog(conversationId);

        if (conversation is null)
        {
            Console.WriteLine($"Nie znaleziono konwersacji o ID: [{conversationId}]");
            
            return NotFound();
        }
        var file = _formService.Generate("Templates/PCC-3(6).xml", conversation.FormModel);
        await _eDeclarationClient.SendForm(file);

        Console.WriteLine($"Wyslano XML do serwisu EDeklaracje dla konwersacji o ID [{conversationId}]");
        
        return Ok();
    }
}