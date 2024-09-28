namespace TaxAssistant.Declarations.Questions;

public static class QuestionsProvider
{
    public static Dictionary<int, string> GetNotAnsweredQuestions(Dictionary<int, string> questionsPool, int [] answeredQuestionsIds)
    {
        var notAnsweredQuestions = questionsPool
            .Where(q => !answeredQuestionsIds.Contains(q.Key))
            .ToDictionary(q => q.Key, q => q.Value);
        
        return notAnsweredQuestions;
    }
}