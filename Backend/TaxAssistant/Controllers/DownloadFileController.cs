using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Models;
using TaxAssistant.Models;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class DownloadFileController : ControllerBase
{
    private readonly ConversationReader _conversationReader;
    private readonly IFormService _formService;

    public DownloadFileController(ConversationReader conversationReader, IFormService formService)
    {
        _conversationReader = conversationReader;
        _formService = formService;
    }

    [HttpGet("download-file/{conversationId}")]
    public async Task<IActionResult> Get(string conversationId)
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

    private async Task<DeclarationFileResponse> GenerateFileAsync(FormModel formModel)
    {
        var xml = _formService.Generate("Templates/PCC-3(6).xml", formModel);

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var fileName = $"declaration_{timestamp}.xml";

        using var stream = new MemoryStream();
        using var reader = new StreamWriter(stream);
        await reader.WriteAsync(xml);

        return new DeclarationFileResponse(stream.ToArray(), fileName);
    }
}