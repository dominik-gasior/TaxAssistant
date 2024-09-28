namespace TaxAssistant.Prompts;

public static class PromptsProvider
{
    private const string LanguageInstruction = "Odpowiedz musi byc przetlumaczona na jezyk WIADOMOOSCI UZYTKOWNIKA";

    private const string BlockChangingTheTopicInstruction =
        """
        Jesli WIADOMOOSC UZYTKOWNIKA nie dotyczy podatkow odpowiedz ze mozesz odpowiedziec na pytania tylko z nimi zwiazane
        """;
        
    public static string QuestionsResponseChecker(string message)
    {
        return 
            $$"""
                 Z podanej wiadomosci przygotuj filtry w JSONie:
                 Filtry nie moga zawierac innych pol niz te przedstawione ponizej, w momencie gdy nie 
                 jestes w stanie okreslic wartosci filtru wartosc musi wynosic null
                 
                 WIADOMOSC UZYTKOWNIKA
                 '''{{message}}'''
                 KONIEC WIADOMOSCI
                 
                 Odpowiedz używając poniższego formatu:
              
                  {
                    date_of_action: "2024-01-01",
                    office_name: "Urzad Skarbowy w Zawierciu",
                    entity_submitting_action: true,
                    taxpayer_type: "individual",
                    taxpayer_data: {
                      first_name: "Jan",
                      last_name: "Kowalski",
                      pesel: "12345678900",
                      date_of_birth: "2000-01-01",
                    },
                    address: {
                      country: "PL",
                      province: "Slask",
                      county: "Zawiercianski",
                      municipality: "",
                      street: "3 Maja",
                      house_number: "12",
                      apartment_number: "2",
                      city: "Ogrodzieniec",
                      postal_code: "42-440",
                    },
                    action_description: "Zakup auta",
                    amount: 1000,
                  }
                
                Przygotuj filtry na podstawie wiadomosci:
              """;
    }

    
    public static string DeclarationClassification(string message, string opisDeklaracji)
    {
        return 
        $$"""
           Z podanej wiadomosci przygotuj filtr w JSONie:
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
    
    public static string DeclarationIsReadyToConfirm(string message)
    {
        return
        $$"""
        Wygeneruj wiadomosc skierowana do uzytkownika
        {{LanguageInstruction}}
        
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
    
    public static string NextQuestion(string nextQuestion, string message)
    {
        return
        $"""
        Twoja rola jest zapytanie sie uzytkownika o podana kwestie
        WIADOMOSC UZYTKOWNIKA
        '''{message}'''
        KONIEC WIADOMOSCI
        
        {nextQuestion}
        """;
    }

    public static string DetectedDeclarationFormat(string typDeklaracji, string message)
    {
        return 
        $"""
         Poinformuj uzytkownika ze jego sprawa moze zostac zrealizowana przy pomocy deklaracji {typDeklaracji}
         {LanguageInstruction}
         Zapytaj sie uzytkownika czy chce kontynuowac
         
         WIADOMOSC UZYTKOWNIKA
         '''{message}'''
         KONIEC WIADOMOSCI
         
         Odpowiedz: Wiadomosc skierowana do uzytkownika
         """;
    }
    
    public static string NoMatchingDeclarationType(string message)
    {
        return 
            $"""
             Poinformuj uzytkownika ze jego sprawa nie jest obecnie obslugiwana przez czat, oraz ze moze sprobowac w przyszlosci
             {LanguageInstruction}
             
             WIADOMOSC UZYTKOWNIKA
             '''{message}'''
             KONIEC WIADOMOSCI

             Odpowiedz: Wiadomosc skierowana do uzytkownika
             """;
    }
}