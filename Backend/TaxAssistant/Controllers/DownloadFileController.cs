using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class DownloadFileController : ControllerBase
{
    private readonly IDeclarationService _declarationService;

    public DownloadFileController(IDeclarationService declarationService)
    {
        _declarationService = declarationService;
    }

    [HttpPost("download-file")]
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