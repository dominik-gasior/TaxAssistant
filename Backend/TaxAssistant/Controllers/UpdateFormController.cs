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

    [HttpPut("update-form")]
    public async Task<IActionResult> Put([FromBody] FormFile formFile)
    {
        throw new NotImplementedException();
    }
}