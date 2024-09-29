using Microsoft.AspNetCore.Mvc;
using TaxAssistant.External.Clients;
using TaxAssistant.Models;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers;

[ApiController]
[Route("api/send-form")]
public class SendFormController : ControllerBase
{
    private readonly IFormService _formService;
    private readonly EDeclarationClient _eDeclarationClient;

    public SendFormController(IFormService formService, EDeclarationClient eDeclarationClient)
    {
        _formService = formService;
        _eDeclarationClient = eDeclarationClient;
    }

    [HttpPost]
    public async Task<IActionResult> GetDeclarationFileAsync([FromBody] FormModel model)
    {
        var file = _formService.Generate("Templates/PCC-3(6).xml", model);
        await _eDeclarationClient.SendForm(file);
        return Ok();
    }
}