using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Models;
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
    public async Task<IActionResult> GenerateLlmResponse([FromBody] GenerateLlmRequest request)
    {
        if (request.IsInitialMessage)
        {
            return Ok(await _declarationService.GetCorrectDeclarationTypeAsync(request.UserMessage));
        }

        var result = await _declarationService.GenerateQuestionAboutNextMissingFieldAsync
        (
            request.DeclarationType,
            request.UserMessage
        );
        
        return Ok(result);
    }
}
