using System.Text.Json;
using TaxAssistant.Models;

namespace TaxAssistant.Services;

public class FormModelValidator
{
    public static FormModel UpdateFormModel(FormModel oldModel, FormModel newModel)
    {
        if (newModel.TaxpayerType is null) newModel = newModel with { TaxpayerType = oldModel.TaxpayerType };
        var valid = ValidateForm(newModel);

        var address = UpdateAddress(oldModel.Address, newModel.Address, valid);
        Taxpayer taxpayer = new();
        Taxpayer companyTaxpayer = new();
        if (newModel.TaxpayerType == "individual")
        {
            var newIndividualTaxpayer = JsonSerializer.Deserialize<Taxpayer>(JsonSerializer.Serialize(newModel.TaxpayerData)) ?? new Taxpayer();
            var oldIndividualTaxpayer = oldModel.TaxpayerType == "individual"
                ? JsonSerializer.Deserialize<Taxpayer>(JsonSerializer.Serialize(oldModel.TaxpayerData)) ?? new Taxpayer()
                : new Taxpayer();
            taxpayer = UpdateIndividualTaxpayer(oldIndividualTaxpayer, newIndividualTaxpayer, valid);
        }
        if (newModel.TaxpayerType == "company")
        {
            var newIndividualTaxpayer = JsonSerializer.Deserialize<Taxpayer>(JsonSerializer.Serialize(newModel.TaxpayerData)) ?? new Taxpayer();
            var oldIndividualTaxpayer = oldModel.TaxpayerType == "company"
                ? JsonSerializer.Deserialize<Taxpayer>(JsonSerializer.Serialize(newModel.TaxpayerData)) ?? new Taxpayer()
                : new Taxpayer();
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
            TaxpayerData = newModel.TaxpayerType == "individual" ? taxpayer : companyTaxpayer
            
        };

        return result;
    }
    
    private static Taxpayer UpdateIndividualTaxpayer(Taxpayer old, Taxpayer @new, List<string> valid) => new()
    {
        FirstName = valid.Contains(nameof(old.FirstName)) ? @new.FirstName : old.FirstName,
        LastName = valid.Contains(nameof(old.LastName)) ? @new.LastName : old.LastName,
        Pesel = valid.Contains(nameof(old.Pesel)) ? @new.Pesel : old.Pesel,
        DateOfBirth = valid.Contains(nameof(old.DateOfBirth)) ? @new.DateOfBirth : old.DateOfBirth,
    };
    
    private static Taxpayer UpdateCompanyTaxpayer(Taxpayer oldCompany, Taxpayer newCompany, List<string> valid) => new()
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
        var companyData = JsonSerializer.Deserialize<Taxpayer>(JsonSerializer.Serialize(companyObject));
        if (companyData is null) return [];
        var validProps = new List<string>();
        if (!string.IsNullOrWhiteSpace(companyData.FullName)) validProps.Add(nameof(companyData.FullName));
        if (!string.IsNullOrWhiteSpace(companyData.ShortName)) validProps.Add(nameof(companyData.ShortName));
        if (companyData.NIP?.Length is 10) validProps.Add(nameof(companyData.ShortName));

        return validProps;
    }
    
    private static List<string> ValidateIndividualTaxpayer(object? individualObject)
    {
        var individualData = JsonSerializer.Deserialize<Taxpayer>(JsonSerializer.Serialize(individualObject));
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