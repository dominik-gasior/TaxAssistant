using TaxAssistant.Models;

namespace TaxAssistant.Declarations.Models;

public record NextQuestionGenerationResponse(string? DeclarationType, FormModel? FormData, string Message);