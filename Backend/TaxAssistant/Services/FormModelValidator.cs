using System.Text.Json;
using TaxAssistant.Models;

namespace TaxAssistant.Services;

public class FormModelValidator
{
    public static FormModel UpdateFormModel(FormModel oldModel, FormModel newModel)
    {
        var valid = ValidateForm(newModel);

        var address = UpdateAddress(oldModel.Address, newModel.Address, valid);
        IndividualTaxpayer individualTaxpayer = new();
        CompanyTaxpayer companyTaxpayer = new();
        if (newModel.TaxpayerType == "individual")
        {
            var newIndividualTaxpayer = JsonSerializer.Deserialize<IndividualTaxpayer>(JsonSerializer.Serialize(newModel.TaxpayerData)) ?? new IndividualTaxpayer();
            var oldIndividualTaxpayer = oldModel.TaxpayerType == "individual"
                ? JsonSerializer.Deserialize<IndividualTaxpayer>(JsonSerializer.Serialize(newModel.TaxpayerData)) ?? new IndividualTaxpayer()
                : new IndividualTaxpayer();
            individualTaxpayer = UpdateIndividualTaxpayer(oldIndividualTaxpayer, newIndividualTaxpayer, valid);
        }
        if (newModel.TaxpayerType == "company")
        {
            var newIndividualTaxpayer = JsonSerializer.Deserialize<CompanyTaxpayer>(JsonSerializer.Serialize(newModel.TaxpayerData)) ?? new CompanyTaxpayer();
            var oldIndividualTaxpayer = oldModel.TaxpayerType == "company"
                ? JsonSerializer.Deserialize<CompanyTaxpayer>(JsonSerializer.Serialize(newModel.TaxpayerData)) ?? new CompanyTaxpayer()
                : new CompanyTaxpayer();
            companyTaxpayer = UpdateCompanyTaxpayer(oldIndividualTaxpayer, newIndividualTaxpayer, valid);
        }
        
        var result = oldModel with
        {
            ActionDescription = valid.Contains(nameof(oldModel.ActionDescription)) ? newModel.ActionDescription : oldModel.ActionDescription,
            Amount = valid.Contains(nameof(oldModel.Amount)) ? newModel.Amount : oldModel.Amount,
            DateOfAction = valid.Contains(nameof(oldModel.DateOfAction)) ? newModel.DateOfAction : oldModel.DateOfAction,
            EntitySubmittingAction = valid.Contains(nameof(oldModel.EntitySubmittingAction)) ? newModel.EntitySubmittingAction : oldModel.EntitySubmittingAction,
            OfficeName = valid.Contains(nameof(oldModel.OfficeName)) ? newModel.OfficeName : oldModel.OfficeName,
            TaxpayerType = valid.Contains(nameof(oldModel.TaxpayerType)) ? newModel.TaxpayerType : oldModel.TaxpayerType,
            Address = address,
            TaxpayerData = newModel.TaxpayerType == "individual" ? individualTaxpayer : companyTaxpayer
            
        };

        return result;
    }
    
    private static IndividualTaxpayer UpdateIndividualTaxpayer(IndividualTaxpayer oldIndividual, IndividualTaxpayer newIndividual, List<string> valid) => new()
    {
        FirstName = valid.Contains(nameof(oldIndividual.FirstName)) ? newIndividual.FirstName : oldIndividual.FirstName,
        LastName = valid.Contains(nameof(oldIndividual.LastName)) ? newIndividual.LastName : oldIndividual.LastName,
        Pesel = valid.Contains(nameof(oldIndividual.Pesel)) ? newIndividual.Pesel : oldIndividual.Pesel,
        DateOfBirth = valid.Contains(nameof(oldIndividual.DateOfBirth)) ? newIndividual.DateOfBirth : oldIndividual.DateOfBirth,
    };
    
    private static CompanyTaxpayer UpdateCompanyTaxpayer(CompanyTaxpayer oldCompany, CompanyTaxpayer newCompany, List<string> valid) => new()
    {
        FullName = valid.Contains(nameof(oldCompany.FullName)) ? newCompany.FullName : oldCompany.FullName,
        ShortName = valid.Contains(nameof(oldCompany.ShortName)) ? newCompany.ShortName : oldCompany.ShortName,
        NIP = valid.Contains(nameof(oldCompany.NIP)) ? newCompany.NIP : oldCompany.NIP,
    };

    private static Address UpdateAddress(Address oldAddress, Address newAddress, List<string> valid) => new()
    {
        Country = valid.Contains(nameof(oldAddress.Country)) ? newAddress.Country : oldAddress.Country,
        Province = valid.Contains(nameof(oldAddress.Province)) ? newAddress.Province : oldAddress.Province,
        PostalCode = valid.Contains(nameof(oldAddress.PostalCode)) ? newAddress.PostalCode : oldAddress.PostalCode,
        Street = valid.Contains(nameof(oldAddress.Street)) ? newAddress.Street : oldAddress.Street,
        ApartmentNumber = valid.Contains(nameof(oldAddress.ApartmentNumber)) ? newAddress.ApartmentNumber : oldAddress.ApartmentNumber,
        Municipality = valid.Contains(nameof(oldAddress.Municipality)) ? newAddress.Municipality : oldAddress.Municipality,
        County = valid.Contains(nameof(oldAddress.County)) ? newAddress.County : oldAddress.County,
        HouseNumber = valid.Contains(nameof(oldAddress.HouseNumber)) ? newAddress.HouseNumber : oldAddress.HouseNumber,
        City = valid.Contains(nameof(oldAddress.City)) ? newAddress.City : oldAddress.City
    };

    public static List<string> ValidateForm(FormModel formModel)
    {
        //TODO: officeName
        var validProps = new List<string>();
        if (formModel.DateOfAction > DateOnly.FromDateTime(DateTime.Now.AddDays(-14))) validProps.Add(nameof(formModel.DateOfAction));
        if (new EterytFiles().officies.Contains(formModel.OfficeName ?? "")) validProps.Add(nameof(formModel.OfficeName));
        if (formModel.EntitySubmittingAction is <= 5 and > 1) validProps.Add(nameof(formModel.EntitySubmittingAction));
        if (formModel.TaxpayerType is "individual" or "company") validProps.Add(nameof(formModel.TaxpayerType));
        if (formModel.ActionDescription?.Length is > 0 and < 3500) validProps.Add(nameof(formModel.ActionDescription));
        if (formModel.Amount is > 0 ) validProps.Add(nameof(formModel.Amount));
        
        if (formModel.TaxpayerType is "company")
        {
            var validateCompanyTaxpayerResult = ValidateCompanyTaxpayer(formModel.TaxpayerData);
            validProps.AddRange(validateCompanyTaxpayerResult);
        }
        if (formModel.TaxpayerType is "individual")
        {
            var validateIndividualTaxpayerResult = ValidateIndividualTaxpayer(formModel.TaxpayerData);
            validProps.AddRange(validateIndividualTaxpayerResult);
        }
        
        var validateAddressResult = ValidateAddress(formModel.Address);
        validProps.AddRange(validateAddressResult);

        return validProps;
    }

    private static List<string> ValidateCompanyTaxpayer(object? companyObject)
    {
        var companyData = JsonSerializer.Deserialize<CompanyTaxpayer>(JsonSerializer.Serialize(companyObject));
        if (companyData is null) return [];
        var validProps = new List<string>();
        if (!string.IsNullOrWhiteSpace(companyData.FullName)) validProps.Add(nameof(companyData.FullName));
        if (!string.IsNullOrWhiteSpace(companyData.ShortName)) validProps.Add(nameof(companyData.ShortName));
        if (companyData.NIP?.Length is 10) validProps.Add(nameof(companyData.ShortName));

        return validProps;
    }
    
    private static List<string> ValidateIndividualTaxpayer(object? individualObject)
    {
        var individualData = JsonSerializer.Deserialize<IndividualTaxpayer>(JsonSerializer.Serialize(individualObject));
        if (individualData is null) return [];
        var validProps = new List<string>();

        if (!string.IsNullOrWhiteSpace(individualData.FirstName)) validProps.Add(nameof(individualData.FirstName));
        if (!string.IsNullOrWhiteSpace(individualData.LastName)) validProps.Add(nameof(individualData.LastName));
        if (individualData.Pesel?.Length is 11) validProps.Add(nameof(individualData.Pesel));
        if (individualData.DateOfBirth > DateOnly.FromDateTime(DateTime.Now.AddYears(-18))) validProps.Add(nameof(individualData.DateOfBirth));
        
        return validProps;
    }
    
    private static List<string> ValidateAddress(Address address)
    {
        var validProps = new List<string>();
        if (!string.IsNullOrWhiteSpace(address.Country)) validProps.Add(nameof(address.Country));
        if (!string.IsNullOrWhiteSpace(address.Province)) validProps.Add(nameof(address.Province));
        if (!string.IsNullOrWhiteSpace(address.County)) validProps.Add(nameof(address.County));
        if (!string.IsNullOrWhiteSpace(address.Municipality)) validProps.Add(nameof(address.Municipality));
        if (!string.IsNullOrWhiteSpace(address.Street)) validProps.Add(nameof(address.Street));
        if (!string.IsNullOrWhiteSpace(address.HouseNumber)) validProps.Add(nameof(address.HouseNumber));
        if (!string.IsNullOrWhiteSpace(address.ApartmentNumber)) validProps.Add(nameof(address.ApartmentNumber));
        if (!string.IsNullOrWhiteSpace(address.City)) validProps.Add(nameof(address.City));
        if (!string.IsNullOrWhiteSpace(address.PostalCode)) validProps.Add(nameof(address.PostalCode));
        
        return validProps;
    }
}