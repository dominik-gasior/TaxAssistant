using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Models;
using TaxAssistant.Declarations.Strategies.Interfaces;
using TaxAssistant.External.Services;
using TaxAssistant.Models;
using TaxAssistant.Prompts;

namespace TaxAssistant.Declarations.Services;

public interface IDeclarationService
{
    Task<NextQuestionGenerationResponse> GetCorrectDeclarationTypeAsync(string userMessage);
    Task<NextQuestionGenerationResponse> GenerateQuestionAboutNextMissingFieldAsync(string? declarationType, string userMessage);
}

public class DeclarationService : IDeclarationService
{
    private readonly ILLMService _llmService;
    private readonly IEnumerable<IDeclarationStrategy> _strategies;

    public DeclarationService(ILLMService llmService, IEnumerable<IDeclarationStrategy> strategies)
    {
        _llmService = llmService;
        _strategies = strategies;
    }

    public async Task<NextQuestionGenerationResponse> GetCorrectDeclarationTypeAsync([FromBody] string userMessage)
    {
        foreach (var strategy in _strategies)
        {
            var classificationResult = await strategy.ClassifyAsync(userMessage);

            if (classificationResult)
            {
                var formModelWrapper = await GenerateQuestionAboutNextMissingFieldAsync(strategy.DeclarationType, userMessage);
                
                return new NextQuestionGenerationResponse
                (
                    strategy.DeclarationType,
                    formModelWrapper.FormData,
                    formModelWrapper.Message
                );
            }
        }

        return new NextQuestionGenerationResponse
        (
            "OTHER", 
            null,
            await _llmService.GenerateMessageAsync(PromptsProvider.NoMatchingDeclarationType(userMessage), "text")
        );
    }
    
    public async Task<NextQuestionGenerationResponse> GenerateQuestionAboutNextMissingFieldAsync(string? declarationType, string userMessage)
    {
        var formDataExtraction = await _llmService.GenerateMessageAsync
        (
            PromptsProvider.QuestionsResponseChecker(userMessage, declarationType)
        );

        var formModelWrapper = JsonSerializer.Deserialize<FormModelWrapper>(formDataExtraction);
        
        if (!formModelWrapper!.Questions.Any())
        {
            var message = await _llmService.GenerateMessageAsync(PromptsProvider.DeclarationIsReadyToConfirm(userMessage), "text");
            
            return new NextQuestionGenerationResponse(declarationType, formModelWrapper.FormModel, message);
        }

        return new NextQuestionGenerationResponse(declarationType, formModelWrapper.FormModel, formModelWrapper.Questions.First());
    }
}