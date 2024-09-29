using System.Text.Json.Serialization;

namespace TaxAssistant.Models;

public record FormModelWrapper
{
    [JsonPropertyName("form")]
    public FormModel FormModel { get; set; }
    
    [JsonPropertyName("questions_about_missing_fields")]
    public string[] Questions { get; set; }
}

public record FormModel
{
    [JsonPropertyName("date_of_action")] //P_4 | Data | max 2 tygodnie format 2024-07-29
    public DateOnly? DateOfAction { get; init; }

    [JsonPropertyName("office_name")] //P_5 | KodUrzedu | map this to code
    public string? OfficeName { get; init; }

    [JsonPropertyName("entity_submitting_action")] //P_7 | PozycjeSzczegolowe | 1-5
    public int? EntitySubmittingAction { get; init; }

    [JsonPropertyName("taxpayer_type")] //P_8 | OsobaNiefizyczna lub OsobaFizyczna | enum
    public string? TaxpayerType { get; init; } //"individual" | "company"

    [JsonPropertyName("address")]
    public Address Address { get; init; } = new();

    [JsonPropertyName("action_description")] //P_23 | PozycjeSzczegolowe | sekcja C max 3500 znakow
    public string? ActionDescription { get; init; }

    [JsonPropertyName("amount")] //P_26 | PozycjeSzczegolowe | min 1
    public int? Amount { get; init; }

    [JsonPropertyName("taxpayer_data")] //Unia
    public Taxpayer? TaxpayerData { get; init; }
}

public record Address
{
    [JsonPropertyName("country")] //P_11 | KodKraju | Baza
    public string? Country { get; init; }

    [JsonPropertyName("province")] //P_12 | Wojewodztwo | Baza
    public string? Province { get; init; }

    [JsonPropertyName("county")] //P_13 | Powiat | Baza
    public string? County { get; init; }

    [JsonPropertyName("municipality")] //P_14 | Gmina | Baza
    public string? Municipality { get; init; }

    [JsonPropertyName("street")] //P_15 | Ulica | Baza
    public string? Street { get; init; }

    [JsonPropertyName("house_number")] //P_16 | NrDomu | Baza
    public string? HouseNumber { get; init; }

    [JsonPropertyName("apartment_number")] //P_17 | NrLokalu | Baza
    public string? ApartmentNumber { get; init; }

    [JsonPropertyName("city")] //P_18 | Miejscowosc | Baza
    public string? City { get; init; }

    [JsonPropertyName("postal_code")] //P_19 | KodPocztowy | Baza + validacja
    public string? PostalCode { get; init; }
}

public record Taxpayer
{
    [JsonPropertyName("first_name")] //P_8 | ImiePierwsze |
    public string? FirstName { get; init; }

    [JsonPropertyName("last_name")] //P_8 | Nazwisko |
    public string? LastName { get; init; }

    [JsonPropertyName("pesel")] //P_8 | PESEL | validacja
    public string? Pesel { get; init; }

    [JsonPropertyName("date_of_birth")] //P_8 | DataUrodzenia | validacja + format 1954-12-18
    public DateOnly? DateOfBirth { get; init; }
    
    [JsonPropertyName("full_name")] //P_8 | PelnaNazwa |
    public string? FullName { get; init; }

    [JsonPropertyName("short_name")] //P_8 | SkroconaNazwa |
    public string? ShortName { get; init; }

    [JsonPropertyName("nip")] // //P_8 | NIP | validacja
    public string? NIP { get; init; }
}