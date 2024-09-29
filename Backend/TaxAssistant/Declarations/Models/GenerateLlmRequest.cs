namespace TaxAssistant.Declarations.Models;

public record GenerateLlmRequest
(
    string UserMessage, 
    bool IsInitialMessage, 
    string? DeclarationType
);