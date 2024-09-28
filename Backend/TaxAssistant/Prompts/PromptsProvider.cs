namespace TaxAssistant.Prompts;

public static class PromptsProvider
{
    public static string DeclarationClassification(string message, string opisDeklaracji)
    {
        return 
        $$"""
           Z podanej waidomosci przygotuj filtr w JSONie:
           Na podstawie opisu przeznaczenia deklaracji podatkowej sklasyfikuj czy podane przez uzytkownika informacje podlegaja podanej deklaracji
           
           WIADOMOSC UZYTKOWNIKA
           '''{{message}}'''
           KONIEC WIADOMOSCI
           
           OPIS DEKLARACJI
           '''{{opisDeklaracji}}'''
           KONIEC OPISU DEKLRACJI
           
           Odpowiedz używając poniższego formatu:
        
           {
               "isGoodMatch": true
           }
          
          Przygotuj filtr na podstawie wiadomosci:
        """;
    }
    
    public static string QuestionsClassification()
    {
        return "";
    }
    
    public static string DeclarationIsReadyToConfirm()
    {
        return
        """
        Wygeneruj wiadomosc skierowana do uzytkownika
        
        Treść wiadomości:
        Wskazanie, że wszystkie pola w formularzu zostały wypełnione.
        Informacja, że deklaracja jest gotowa do złożenia w Urzędzie Skarbowym.
        Na koniec wiadomosci dodaj, ze wiadomosc jest skierowana przez Urzad Skarbowy
        W wiadomosci poinformuj uzytkownika, ze aby dokonczyc proces zlozenia deklarcji musi kliknac przycisk wyslij 
        
        Zakazy:
        Nie podawaj danych kontaktowych.
        Nie proponuj kontaktu telefonicznego z Urzędem Skarbowym.
        
        Odpowiedz: Wiadomosc skierowana do uzytkownika
        """;
    }
    
    public static string NextQuestion(string nextQuestion)
    {
        return
        $"""
        Twoja rola jest zapytanie sie uzytkownika o podana kwestie
        {nextQuestion}
        """;
    }
}