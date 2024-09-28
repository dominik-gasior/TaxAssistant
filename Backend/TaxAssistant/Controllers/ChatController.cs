using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class ChatController : ControllerBase
{
    private readonly IDeclarationService _declarationService;

    public ChatController(IDeclarationService declarationService)
    {
        _declarationService = declarationService;
    }

    [HttpGet("restore-chat")]
    public async Task<IActionResult> Get([FromBody] FormFile formFile)
    {
        throw new NotImplementedException();
    }
}