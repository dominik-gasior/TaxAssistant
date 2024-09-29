using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Models;
using TaxAssistant.Declarations.Services;
using TaxAssistant.Models;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers;

[ApiController]
public class TaxAssistantController : ControllerBase
{
    private readonly IDeclarationService _declarationService;
    private readonly ConversationDumper _conversationDumper;
    private readonly ConversationReader _conversationReader;
    
    public TaxAssistantController(IDeclarationService declarationService,
        ConversationDumper conversationDumper,
        ConversationReader conversationReader)
    {
        _declarationService = declarationService;
        _conversationDumper = conversationDumper;
        _conversationReader = conversationReader;
    }
    
    [HttpPost("ask-tax-assistant/{conversationId}")]
    public async Task<IActionResult> GenerateLlmResponse(string conversationId, [FromBody] GenerateLlmRequest request)
    {
        Console.WriteLine("Rozpoczecie generowania odpowiedzi przez asystenta");
        
        var userRequestTimestamp = DateTime.Now.Ticks;
        if (request.IsInitialMessage)
        {
            var nextQuestionGenerationResponse = await _declarationService.GetCorrectDeclarationTypeAsync(request.UserMessage);
            await DumpFlow(conversationId, request, nextQuestionGenerationResponse, userRequestTimestamp);
            return Ok(nextQuestionGenerationResponse);
        }

        var result = await _declarationService.GenerateQuestionAboutNextMissingFieldAsync
        (
            request.DeclarationType,
            request.UserMessage
        );
        var conversation = await _conversationReader.GetLatestConversationLog(conversationId);
        if (conversation is null) 
        {
            await DumpFlow(conversationId, request, result, userRequestTimestamp);
        }
        else
        {
            var updatedConversation = conversation with { FormModel = result.FormData ?? conversation.FormModel };
            updatedConversation.ChatLog.Add(new Message
            {
                Content = request.UserMessage,
                Role = Roles.User,
                TimeStamp = userRequestTimestamp
            });
            updatedConversation.ChatLog.Add(new Message
            {
                Content = result.Message,
                Role = Roles.Bot,
                TimeStamp = DateTime.Now.Ticks
            });
            await _conversationDumper.DumpConversationLog(updatedConversation);
        }
        return Ok(result);
    }

    private async Task DumpFlow(
        string conversationId,
        GenerateLlmRequest request,
        NextQuestionGenerationResponse nextQuestionGenerationResponse,
        long userRequestTimestamp)
    {
        await _conversationDumper.DumpConversationLog(new ConversationData
        {
            Id = conversationId,
            FormModel = nextQuestionGenerationResponse.FormData ?? new FormModel(),
            ChatLog = [new Message
            {
                Content = request.UserMessage,
                Role = Roles.User,
                TimeStamp = userRequestTimestamp
            }, new Message
            {
                Content = nextQuestionGenerationResponse.Message,
                Role = Roles.Bot,
                TimeStamp = DateTime.Now.Ticks
            }]
        });
    }
}
