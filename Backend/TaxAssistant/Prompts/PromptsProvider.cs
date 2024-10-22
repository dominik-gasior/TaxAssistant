namespace TaxAssistant.Prompts;

public static class PromptsProvider
{
    private const string LanguageInstruction = "Odpowiedz musi byc przetlumaczona na jezyk WIADOMOOSCI UZYTKOWNIKA";

    private const string BlockChangingTheTopicInstruction =
        """
        Jesli WIADOMOOSC UZYTKOWNIKA nie dotyczy podatkow staraj sie w odpowiedzi naprowadzic uzytkownika na ten temat
        """;
        
    public static string QuestionsResponseChecker(string message, string declarationType)
    {
        return 
            $$"""
                 Z podanej wiadomosci przygotuj filtry w JSONie:
                 Filtry nie moga zawierac innych pol niz te przedstawione ponizej, w momencie gdy nie 
                 jestes w stanie okreslic wartosci filtru wartosc musi wynosic null
                 
                 Jesli ktores z pol w JSONie nie jest uzupelnione dodaj pytanie o jego podanie do tablicy questions_about_missing_fields
                 
                 Typ wypelnianej deklaracji to {{declarationType}}
                 WIADOMOSC UZYTKOWNIKA
                 '''{{message}}'''
                 KONIEC WIADOMOSCI
                 
                 Pole entity_submitting_action przyjmuje nastepujace wartosci
                 1 - Podmiot zobowiązany solidarnie do zapłaty podatku 
                 2 - Strona umowy zamiany
                 3 - Wspólnik spółki cywilnej
                 4 - Podmiot, o którym mowa w art. 9 pkt 10 lit. b ustawy (pożyczkobiorca)
                 5 - Inny podmiot
                 
                 Uwagi:
                 ustal czy podatnik jest osoba fizyczna czy firma
                 trzymaj sie formatu dat rok-miesiac-dzien
                 pamietaj ze data moze byc podana jako "wczoraj" i powinienes policzyc na podstawie jaki dzis jest dzien
                 poprawny nr pesel ma 11 cyfr a nip 10
                 
                 Odpowiedz używając poniższego formatu:
              
              {
                "questions_about_missing_fields": ["Jaka jest twoja data urodzenia"],
                "form":
                  {
                    "date_of_action": "2024-01-01",
                    "office_name": "Urzad Skarbowy w Zawierciu",
                    "entity_submitting_action": 5,
                    "taxpayer_type": "individual",
                    "taxpayer_data": {
                      "first_name": "Jan",
                      "last_name": "Kowalski",
                      "pesel": "12345678900",
                      "date_of_birth": "1999-01-28",
                      "nip": "1234323456",
                      "full_name": "fegmention fault",
                      "short_name": "ff"
                    },
                    "address": {
                      "country": "PL",
                      "province": "Slask",
                      "county": "Zawiercianski",
                      "municipality": "",
                      "street": "3 Maja",
                      "house_number": "12",
                      "apartment_number": "2",
                      "city": "Ogrodzieniec",
                      "postal_code": "42-440",
                    },
                    "action_description": "Zakup auta",
                    "amount": 1000,
                  }
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
        {{BlockChangingTheTopicInstruction}}
        
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
    
    public static string DetectedDeclarationFormat(string typDeklaracji, string message)
    {
        return 
        $$"""
         Poinformuj uzytkownika ze jego sprawa moze zostac zrealizowana przy pomocy deklaracji {typDeklaracji}
         {{LanguageInstruction}}
         {{BlockChangingTheTopicInstruction}}
         Zapytaj sie uzytkownika czy chce kontynuowac
         
         WIADOMOSC UZYTKOWNIKA
         '''{message}'''
         KONIEC WIADOMOSCI
         
         Odpowiedz: Wiadomosc skierowana do uzytkownika
         """;
    }
    
    public static string Summarize(string userMessage, string botMessage)
    {
        return 
            $$"""
             Stworz wiadomosc z polaczenia dwoch ponizej, poinformuj uzytkownika ktora ktora deklaracja jest odpowiednia dla jego sprawy ogranicz odpowiedz do 1 zdania
             {{LanguageInstruction}}
             {{BlockChangingTheTopicInstruction}}

             WIADOMOSC UZYTKOWNIKA
             '''{userMessage}'''
             KONIEC WIADOMOSCI
             
             WIADOMOSC BOTA
             '''{botMessage}'''
             KONIEC WIADOMOSCI

             Odpowiedz: Wiadomosc skierowana do uzytkownika
             """;
    }
    
    public static string NoMatchingDeclarationType(string message)
    {
        return 
            $$"""
             Poinformuj uzytkownika ze jego sprawa nie jest obecnie obslugiwana przez czat, oraz ze moze sprobowac w przyszlosci
             {LanguageInstruction}
             {{BlockChangingTheTopicInstruction}}
             
             WIADOMOSC UZYTKOWNIKA
             '''{message}'''
             KONIEC WIADOMOSCI

             Odpowiedz: Wiadomosc skierowana do uzytkownika
             """;
    }
}