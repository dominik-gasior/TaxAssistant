using TaxAssistant.Declarations.Models;

namespace TaxAssistant.Declarations.FakeData;

public static class DeclarationsProvider
{
    public static Declaration GetDeclaration()
    {
        return new Declaration
        {
            Header = new Header
            {
                FormCode = new FormCode
                {
                    SystemCode = "PCC-3 (6)",
                    TaxCode = "PCC",
                    ObligationType = "Z",
                    SchemaVersion = "1-0E",
                    Value = "PCC-3"
                },
                FormVariant = 6,
                SubmissionPurpose = 1,
                Date = new DateTime(2024, 07, 29),
                OfficeCode = 271
            },
            Entity1 = new Entity1
            {
                Individual = new Individual
                {
                    PESEL = "54121832134",
                    FirstName = "KAMIL",
                    LastName = "WIRTUALNY",
                    DateOfBirth = new DateTime(1954, 12, 18)
                },
                Address = new Address
                {
                    AddressType = "RAD",
                    PolishAddress = new PolishAddress
                    {
                        CountryCode = "PL",
                        Province = "ŚLĄSKIE",
                        District = "M. KATOWICE",
                        Municipality = "M. KATOWICE",
                        Street = "ALPEJSKA",
                        HouseNumber = "6",
                        ApartmentNumber = "66",
                        City = "KATOWICE",
                        PostalCode = "66-666"
                    }
                }
            },
            DetailedPositions = new DetailedPositions
            {
                P7 = 2,
                P20 = 1,
                P21 = 1,
                P22 = 1,
                P23 = "Sprzedałem auto",
                P24 = 10000,
                P25 = 100,
                P46 = 100,
                P53 = 100,
                P62 = 1
            },
            Instructions = 1
        };
    }
}