using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Services;

namespace TaxAssistant.Controllers;

[ApiController]
[Route("api/openai")]
public class OpenAIController : ControllerBase
{
    private readonly IDeclarationService _declarationService;
    
    public OpenAIController(IDeclarationService declarationService)
    {
        _declarationService = declarationService;
    }
    
    [HttpPost("generate-llm-response")]
    public async Task<IActionResult> GenerateLlmResponse(string userMessage, bool isInitialMessage, string declarationType)
    {
        if (isInitialMessage)
        {
            return Ok(await _declarationService.GetCorrectDeclarationTypeAsync(userMessage));
        }
        
        return Ok(await _declarationService.GenerateQuestionAboutNextMissingFieldAsync(declarationType, userMessage));
    }
    
    [HttpPost("generate-declaration-file")]
    public async Task<IActionResult> GetDeclarationFileAsync([FromBody] FormFile formFile)
    {
        var declaration = await _declarationService.GenerateFileAsync(formFile);

        return File
        (
            declaration.Content,
            MediaTypeNames.Application.Xml,
            declaration.FileName
        );
    }
}
