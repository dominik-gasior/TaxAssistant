using Microsoft.AspNetCore.Mvc;
using TaxAssistant.External.Services;

namespace TaxAssistant.Controllers;

[ApiController]
[Route("api/openai")]
public class OpenAIController : ControllerBase
{
    private readonly ILLMService lmmService;

    public OpenAIController(ILLMService lmmService)
    {
        this.lmmService = lmmService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await lmmService.GenerateMessageAsync("Hello", "text"));
    }
}
