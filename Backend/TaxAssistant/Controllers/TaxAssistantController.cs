using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class TaxAssistantController : ControllerBase
{
    private readonly IDeclarationService _declarationService;
    
    public TaxAssistantController(IDeclarationService declarationService)
    {
        _declarationService = declarationService;
    }
    
    [HttpPost("ask-tax-assistant")]
    public async Task<IActionResult> GenerateLlmResponse(string userMessage, bool isInitialMessage, string declarationType)
    {
        if (isInitialMessage)
        {
            return Ok(await _declarationService.GetCorrectDeclarationTypeAsync(userMessage));
        }
        
        return Ok(await _declarationService.GenerateQuestionAboutNextMissingFieldAsync(declarationType, userMessage));
    }
}
