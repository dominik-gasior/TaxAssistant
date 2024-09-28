namespace TaxAssistant.Declarations.Questions;

public class QuestionsProvider
{
    public static readonly Dictionary<int, string> QuestionsPool = new Dictionary<int, string>
    {
        { 1, "Czy formularz, który chcesz wypełnić, to PCC-3 (wersja 6)?" }
    };
    
    public Dictionary<int, string> GetNotAnsweredQuestions(int [] answeredQuestionsIds)
    {
        var notAnsweredQuestions = QuestionsPool
            .Where(q => !answeredQuestionsIds.Contains(q.Key))
            .ToDictionary(q => q.Key, q => q.Value);
        
        return notAnsweredQuestions;
    }
}