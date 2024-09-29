using System.Text.Json;
using TaxAssistant.Declarations.Models;
using TaxAssistant.Declarations.Strategies.Interfaces;
using TaxAssistant.External.Services;
using TaxAssistant.Models;
using TaxAssistant.Prompts;
using TaxAssistant.Services;

namespace TaxAssistant.Declarations.Services;

public interface IDeclarationService
{
    Task<NextQuestionGenerationResponse> GetCorrectDeclarationTypeAsync(string userMessage, string conversationId);
    Task<NextQuestionGenerationResponse> GenerateQuestionAboutNextMissingFieldAsync(
        string? declarationType,
        string userMessage,
        string conversationId);
}

public class DeclarationService : IDeclarationService
{
    private readonly ILLMService _llmService;
    private readonly IEnumerable<IDeclarationStrategy> _strategies;
    private readonly ConversationReader _conversationReader;

    public DeclarationService(ILLMService llmService,
        IEnumerable<IDeclarationStrategy> strategies,
        ConversationReader conversationReader)
    {
        _llmService = llmService;
        _strategies = strategies;
        _conversationReader = conversationReader;
    }

    public async Task<NextQuestionGenerationResponse> GetCorrectDeclarationTypeAsync(string userMessage, string conversationId)
    {
        foreach (var strategy in _strategies)
        {
            var classificationResult = await strategy.ClassifyAsync(userMessage);
            
            if (!classificationResult) continue;
            var formModelWrapper = await GenerateQuestionAboutNextMissingFieldAsync(strategy.DeclarationType, userMessage, conversationId);
            
            var formModelWrapperJson = JsonSerializer.Serialize(formModelWrapper);
            Console.WriteLine($"Dopasowano odpowiedni typ deklaracji [{strategy.DeclarationType}] {formModelWrapperJson}");

            return formModelWrapper with { DeclarationType = strategy.DeclarationType };
        }

        Console.WriteLine("Nie znaleziono odpowiedniego typu deklaracji");
        
        return new NextQuestionGenerationResponse
        (
            "OTHER", 
            null,
            await _llmService.GenerateMessageAsync(PromptsProvider.NoMatchingDeclarationType(userMessage), "text")
        );
    }
    
    public async Task<NextQuestionGenerationResponse> GenerateQuestionAboutNextMissingFieldAsync(string? declarationType, string userMessage, string conversationId)
    {
        var formDataExtraction = await _llmService.GenerateMessageAsync
        (
            PromptsProvider.QuestionsResponseChecker(userMessage, declarationType)
        );

        Console.WriteLine($"Wykryto dane w wiadomosci uzytkownika {formDataExtraction}");
        
        var formModelWrapper = JsonSerializer.Deserialize<FormModelWrapper>(formDataExtraction)!;
        
        var conversation = await _conversationReader.GetLatestConversationLog(conversationId);
        var updatedModel = FormModelValidator.UpdateFormModel(conversation?.FormModel ?? new FormModel(), formModelWrapper.FormModel);

        

        var message = formModelWrapper.Questions.Length is 0 
            ? await _llmService.GenerateMessageAsync(PromptsProvider.DeclarationIsReadyToConfirm(userMessage), "text")
            : formModelWrapper.Questions.First();
        
        return new NextQuestionGenerationResponse(declarationType, updatedModel, message);
    }
}