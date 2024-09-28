using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Declarations.Models;
using TaxAssistant.Declarations.Questions;
using TaxAssistant.Declarations.Strategies.Interfaces;
using TaxAssistant.External.Services;
using TaxAssistant.Prompts;

namespace TaxAssistant.Declarations.Services;

public interface IDeclarationService
{
    Task<GetCorrectDeclarationTypeResponse> GetCorrectDeclarationTypeAsync(string userMessage);
    Task<string> GenerateQuestionAboutNextMissingFieldAsync(string? declarationType, string userMessage);
    Task<DeclarationFileResponse> GenerateFileAsync(FormFile formFile);
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

    public async Task<GetCorrectDeclarationTypeResponse> GetCorrectDeclarationTypeAsync([FromBody] string userMessage)
    {
        foreach (var strategy in _strategies)
        {
            var classificationResult = await strategy.ClassifyAsync(userMessage);

            if (classificationResult)
            {
                return new GetCorrectDeclarationTypeResponse
                (
                    strategy.DeclarationType,
                    await _llmService.GenerateMessageAsync(PromptsProvider.DetectedDeclarationFormat(userMessage, strategy.DeclarationType), "text")
                );
            }
        }

        return  new GetCorrectDeclarationTypeResponse
        (
            "OTHER", 
            await _llmService.GenerateMessageAsync(PromptsProvider.NoMatchingDeclarationType(userMessage), "text")
        );
    }
    
    public async Task<string> GenerateQuestionAboutNextMissingFieldAsync(string? declarationType, string userMessage)
    {
        var questionsCheck = await _llmService.GenerateMessageAsync
        (
            PromptsProvider.QuestionsResponseChecker(userMessage)
        );
        
        //var prompt = PromptsProvider.QuestionsClassification();
        
        var answeredQuestionsIds = new List<int>
        {
            
        };

        var strategy = _strategies.First(s => s.DeclarationType.Equals(declarationType, StringComparison.InvariantCultureIgnoreCase));
        
        var nextQuestions = QuestionsProvider.GetNotAnsweredQuestions(strategy.QuestionsPool, answeredQuestionsIds.ToArray());

        if (nextQuestions.Count == 0)
        {
            return await _llmService.GenerateMessageAsync(PromptsProvider.DeclarationIsReadyToConfirm(userMessage), "text");
        }
        
        var firstMissingQuestion = nextQuestions.First().Value;
        var questionToTheHuman = await _llmService.GenerateMessageAsync(PromptsProvider.NextQuestion(userMessage, firstMissingQuestion), "text");

        return questionToTheHuman;
    }
    
    public Task<DeclarationFileResponse> GenerateFileAsync(FormFile formFile)
    {
        var writer = new XmlSerializer(formFile.GetType());
        var stream = new MemoryStream();
        
        writer.Serialize(stream, formFile);

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var fileName = $"declaration_{timestamp}.xml";

        return Task.FromResult
        (
            new DeclarationFileResponse
            (
                stream.ToArray(), 
                fileName
            )
        );
    }
}