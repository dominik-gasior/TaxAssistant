using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Clients.Llms;

namespace TaxAssistant.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly ILlmClient _llmClient;
    
    public TestController(ILlmClient llmClient)
    {
        _llmClient = llmClient;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _llmClient.GenerateMessageAsync("Hello", "text"));
    }
}
