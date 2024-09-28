using System.Text.Json;
using TaxAssistant.Declarations.Strategies.Interfaces;
using TaxAssistant.External.Services;
using TaxAssistant.Prompts;

namespace TaxAssistant.Declarations.Strategies;

public class PCC3 : IDeclarationStrategy
{
    private readonly ILLMService _llmService;
    
    public PCC3(ILLMService llmService)
    {
        _llmService = llmService;
    }
    
    public Dictionary<int, string> QuestionsPool { get; } = new Dictionary<int, string>
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
    
    Deklaracji nie składa się, gdy:
    
    • czynność cywilnoprawna jest dokonywana w formie aktu notarialnego i podatek jest
    pobierany przez notariusza (płatnika podatku),
    
    • podatnik składa zbiorczą deklarację w sprawie podatku od czynności cywilnoprawnych
    (PCC-4),
    
    • podatnikiem jest:
    
    ◦ kupujący na własne potrzeby sprzęt rehabilitacyjny, wózki inwalidzkie, motorowery,
    motocykle lub samochody osobowe – jeśli ma: orzeczenie o znacznym albo
    umiarkowanym stopniu niepełnosprawności (nieważne, jakie ma schorzenie), o
    orzeczenie o lekkim stopniu niepełnosprawności w związku ze schorzeniami narządów
    ruchu.
    
    ◦ organizacja pożytku publicznego – jeśli dokonuje czynności cywilnoprawnych tylko w
    związku ze swoją nieodpłatną działalnością pożytku publicznego.
    
    ◦ jednostka samorządu terytorialnego,
    
    ◦ Skarb Państwa,
    
    ◦ Agencja Rezerw Materiałowych,
    
    • korzysta się ze zwolnienia od podatku, gdy:
    
    ◦ kupowane są obce waluty,
    
    ◦ kupowane są i zamieniane waluty wirtualne,
    
    ◦ kupowane są rzeczy ruchome – i ich wartość rynkowa nie przekracza 1 000 zł,
    
    ◦ pożyczane jest nie więcej niż 36 120 zł (liczą się łącznie pożyczki z ostatnich 5 lat od
    jednej osoby) – jeśli jest to pożyczka od bliskiej rodziny, czyli od: małżonka, dzieci,
    wnuków, prawnuków, rodziców, dziadków, pradziadków, pasierbów, pasierbic,
    rodzeństwa, ojczyma, macochy, zięcia, synowej, teściów,
    
    ◦ pożyczane są pieniądze od osób spoza bliskiej rodziny – jeśli wysokość pożyczki nie
    przekracza 1 000 zł.
    Deklarację składa się tylko w przypadkach umów, których przedmiotem są rzeczy i prawa majątkowe
    (majątek), znajdujące się w Polsce. A jeśli są za granicą – to tylko jeśli ich nabywca mieszka albo ma
    siedzibę w Polsce i zawarł umowę w Polsce. W przypadku umowy zamiany wystarczy, że w Polsce jest
    jeden z zamienianych przedmiotów.
    """;

    public string DeclarationType { get; } = "PCC3";

    public async Task<bool> ClassifyAsync(string userMessage)
    {
         var classificationPrompt = PromptsProvider.DeclarationClassification(userMessage, Description);
         var response = await _llmService.GenerateMessageAsync(classificationPrompt);

         var classificationResult = JsonSerializer.Deserialize<DeclarationClassification>(response);

         return classificationResult!.IsGoodMatch;
    }
}