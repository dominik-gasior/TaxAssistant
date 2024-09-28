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
    Task<string> GenerateQuestionAboutNextMissingFieldAsync(string userMessage);
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
                    $"Twoja sprawa moze zostac zrealizowana przy pomocy formularza {strategy.DeclarationType}. Czy chcesz kontynuowac?"
                );
            }
        }

        return  new GetCorrectDeclarationTypeResponse
        (
            "OTHER", 
            "Niestety obecnie nie jestesmy w stanie przetworzyc podanego typu wniosku. Sprobuj ponownie pozniej."
        );
    }
    
    public async Task<string> GenerateQuestionAboutNextMissingFieldAsync(string userMessage)
    {
        //var prompt = PromptsProvider.QuestionsClassification();

        var answeredQuestionsIds = new List<int>
        {
            
        };
        
        var nextQuestions = QuestionsProvider.GetNotAnsweredQuestions(answeredQuestionsIds.ToArray());

        if (!nextQuestions.Any())
        {
            return await _llmService.GenerateMessageAsync(PromptsProvider.DeclarationIsReadyToConfirm(), "text");
        }
        
        var firstMissingQuestion = nextQuestions.First().Value;
        var questionToTheHuman = await _llmService.GenerateMessageAsync(PromptsProvider.NextQuestion(firstMissingQuestion), "text");

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