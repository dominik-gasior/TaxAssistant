using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class UpdateFormController : ControllerBase
{
    private readonly IDeclarationService _declarationService;

    public UpdateFormController(IDeclarationService declarationService)
    {
        _declarationService = declarationService;
    }

    [HttpPost("update-form")]
    public async Task<IActionResult> GetDeclarationFileAsync([FromBody] FormFile formFile)
    {
        throw new NotImplementedException();
    }
}