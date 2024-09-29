using Microsoft.AspNetCore.Mvc;
using TaxAssistant.External.Services;
using TaxAssistant.Models;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers;

[ApiController]
[Route("api/send-form")]
public class SendFormController : ControllerBase
{
    private readonly IFormService _formService;
    private readonly IEDeclarationService _eDeclarationService;

    public SendFormController(IEDeclarationService eDeclarationService, IFormService formService)
    {
        _eDeclarationService = eDeclarationService;
        _formService = formService;
    }

    [HttpPost]
    public async Task<IActionResult> GetDeclarationFileAsync([FromBody] FormModel model)
    {
        var file = _formService.Generate("Templates/PCC-3(6).xml", model);
        await _eDeclarationService.SendDeclarationAsync(file);
        return Ok();
    }
}