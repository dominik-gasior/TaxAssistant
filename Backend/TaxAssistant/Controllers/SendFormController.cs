using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class SendFormController : ControllerBase
{
    private readonly IDeclarationService _declarationService;

    public SendFormController(IDeclarationService declarationService)
    {
        _declarationService = declarationService;
    }

    [HttpPost("send-form")]
    public async Task<IActionResult> GetDeclarationFileAsync([FromBody] FormFile formFile)
    {
        throw new NotImplementedException();
    }
}