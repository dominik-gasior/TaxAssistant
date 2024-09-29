using System.Globalization;
using TaxAssistant.Models;
using TaxAssistant.Services;
using VerifyXunit;

namespace TaxAssistant.Tests.Services;

public class FormParserTests
{
    private readonly FormParser _sut = new();
    
    [Fact]
    public async Task FormService_ShouldParseIndividualXml_ForIndividualTaxpayerType()
    {
        // Arrange
        var formData = new FormModel
        {
            TaxpayerType = "individual",
            TaxpayerData = new IndividualTaxpayer
            {
                DateOfBirth = DateOnly.MinValue,
                FirstName = "FirstName",
                LastName = "LastName",
                Pesel = "12343234565"
            },
            Amount = 1000,
            DateOfAction = DateOnly.Parse("2000-12-28", CultureInfo.InvariantCulture),
            EntitySubmittintAction = 5,
            OfficeName = "OfficeName",
            Address = new Address
            {
                City = "City",
                ApartmentNumber = "ApartmentNumber",
                Country = "Country",
                County = "County",
                HouseNumber = "HouseNumber",
                Municipality = "Municipality",
                PostalCode = "01-111",
                Province = "Province",
                Street = "Street"
            },
            ActionDescription = "ActionDescription"
        };
        // Act
        var result = _sut.Generate("Templates/PCC-3(6).xml", formData);
        
        // Assert
        await Verify(result);
    }
    
    [Fact]
    public async Task FormService_ShouldParseIndividualXml_ForCompanyTaxpayerType()
    {
        // Arrange
        // Act
        
        // Assert
    }
}