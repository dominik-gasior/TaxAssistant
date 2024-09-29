using System.Net.Mime;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Models;
using TaxAssistant.Declarations.Services;
using TaxAssistant.Models;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class DownloadFileController : ControllerBase
{
    private ConversationReader _conversationReader;

    public DownloadFileController(ConversationReader conversationReader)
    {
        _conversationReader = conversationReader;
    }

    [HttpGet("download-file")]
    public async Task<IActionResult> GetDeclarationFileAsync(string conversationId)
    {
        var conversation = await _conversationReader.GetLatestConversationLog(conversationId);
        if (conversation is null) return NotFound();
        var declaration = await GenerateFileAsync(conversation.FormModel);

        return File
        (
            declaration.Content,
            MediaTypeNames.Application.Xml,
            declaration.FileName
        );
    }
    
    private Task<DeclarationFileResponse> GenerateFileAsync(FormModel formModel)
    {
        var writer = new XmlSerializer(formModel.GetType());
        var stream = new MemoryStream();
        
        writer.Serialize(stream, formModel);

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