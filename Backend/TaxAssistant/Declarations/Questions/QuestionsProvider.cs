namespace TaxAssistant.Declarations.Questions;

public class QuestionsProvider
{
    public static readonly Dictionary<int, string> QuestionsPool = new Dictionary<int, string>
    {
        { 1, "Czy formularz, który chcesz wypełnić, to PCC-3 (wersja 6)?" },
        { 2, "Jaką wartość ma wariant formularza? Czy to na pewno 6?" },
        { 3, "Czy jest to deklaracja skladana po raz pierwszy" },
        { 4, "Jaką datę wypełnienia formularza chcesz podać?" },
        { 5, "Jaki kod urzędowy chcesz podać dla urzędu skarbowego?" },
        { 6, "Jaki jest Twój numer PESEL?" },
        { 7, "Jakie jest Twoje imię?" },
        { 8, "Jakie jest Twoje nazwisko?" },
        { 9, "Jaką datę urodzenia chcesz podać?" },
        { 10, "Jaki jest twoj adres zamieszkania?" },
        { 11, "Jakiego typu czynność podlega opodatkowaniu? (np. oznacza sprzedaż rzeczy ruchomych)?" },
        { 12, "Czy jesteś pierwszym podatnikiem?" },
        { 13, "Czy jesteś zobowiązany do obliczenia podatku" },
        { 14, "Czy deklaracja jest składana w odpowiednim terminie?" },
        { 15, "Jak opisałbyś, czego dotyczy umowa?" },
        { 16, "Jaka jest wartość przedmiotu transakcji?" },
        { 17, "Czy wartość w polu określającym podstawę opodatkowania jest prawidłowa?" },
        { 18, "Ile wynosi kwota podatku w PLN?" },
        { 19, "Czy formularz został złożony w formie papierowej?" },
        { 20, "Czy zapoznałeś się z pouczeniami?" }
    };
    
    public static Dictionary<int, string> GetNotAnsweredQuestions(int [] answeredQuestionsIds)
    {
        var notAnsweredQuestions = QuestionsPool
            .Where(q => !answeredQuestionsIds.Contains(q.Key))
            .ToDictionary(q => q.Key, q => q.Value);
        
        return notAnsweredQuestions;
    }
}