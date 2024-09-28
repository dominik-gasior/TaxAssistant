using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Services;
using TaxAssistant.External.Services;

namespace TaxAssistant.Controllers;

[ApiController]
[Route("api/openai")]
public class OpenAIController : ControllerBase
{
    private readonly ILLMService _llmService;
    private readonly IDeclarationService _declarationService;
    
    public OpenAIController(ILLMService llmService, IDeclarationService declarationService)
    {
        _llmService = llmService;
        _declarationService = declarationService;
    }

    [HttpPost("generate-llm-response")]
    public async Task<IActionResult> Get()
    {
        var message = await _llmService.GenerateMessageAsync("Hello", "text");
        
        return Ok(message);
    }
    
    [HttpGet]
    [Route("get-declaration-file")]
    public async Task<IActionResult> GetDeclarationFileAsync([FromQuery] Guid declarationId)
    {
        var declaration = await _declarationService.GetDeclarationByIdAsync(declarationId);

        return File
        (
            declaration.Content,
            MediaTypeNames.Application.Xml,
            declaration.FileName
        );
    }

}
