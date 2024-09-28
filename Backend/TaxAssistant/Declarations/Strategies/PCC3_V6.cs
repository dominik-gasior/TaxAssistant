using TaxAssistant.Declarations.Strategies.Interfaces;
using TaxAssistant.External.Services;
using TaxAssistant.Prompts;

namespace TaxAssistant.Declarations.Strategies;

public class PCC3_V6 : IDeclarationStrategy
{
    private readonly ILLMService _llmService;
    
    public PCC3_V6(ILLMService llmService)
    {
        _llmService = llmService;
    }
    
    public string Description { get; } =
    """
    Deklarację składa się w przypadku:
    
    • zawarcia umowy: sprzedaży, zamiany rzeczy i praw majątkowych, pożyczki pieniędzy lub
    rzeczy oznaczonych tylko co do gatunku (jeśli z góry nie zostanie ustalona suma pożyczki –
    deklaracje składa się w przypadku każdorazowej wypłaty środków pieniężnych), o dział
    spadku lub zniesienie współwłasności, gdy dochodzi w nich do spłat i dopłat, ustanowienia
    odpłatnego użytkowania (w tym nieprawidłowego), depozytu nieprawidłowego lub spółki,
    
    • przyjęcia darowizny z przejęciem długów i ciężarów albo zobowiązania darczyńcy,
    
    • złożenia oświadczenia o ustanowieniu hipoteki lub zawarcia umowy ustanowienia hipoteki,
    
    • uprawomocnia się orzeczenia sądu lub otrzymania wyroku sądu polubownego albo zawarcia
    ugody w sprawach umów wyżej wymienionych,
    
    • zawarcia umowy przeniesienia własności – jeśli wcześniej podpisana została umowa
    zobowiązująca do przeniesienia własności, a teraz podpisana została umowa przeniesienia tej
    własności,
    
    • podwyższenia kapitału w spółce mającej osobowość prawną.
    """;

    public string DeclarationType { get; } = "PCC3";

    public async Task<bool> ClassifyAsync(string userMessage)
    {
         var classificationPrompt = PromptsProvider.DeclarationClassification(userMessage);
         var response = await _llmService.GenerateMessageAsync(classificationPrompt, "text");

         var classificationResult = response.Equals("TAK", StringComparison.CurrentCultureIgnoreCase);

         return classificationResult;
    }
}