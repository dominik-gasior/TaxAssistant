using System.Text.Json;
using TaxAssistant.Models;

namespace TaxAssistant.Services;

public class FormModelValidator
{
    public FormModel UpdateFormModel(FormModel oldModel, FormModel newModel)
    {
        var valid = ValidateForm(newModel);



        return oldModel;
    }
    
    public List<string> ValidateForm(FormModel formModel)
    {
        var validProps = new List<string>();
        if (formModel.DateOfAction > DateOnly.FromDateTime(DateTime.Now.AddDays(-14))) validProps.Add(nameof(formModel.DateOfAction));
        if (formModel.OfficeName is "") validProps.Add(nameof(formModel.OfficeName));
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

    private List<string> ValidateCompanyTaxpayer(object? companyObject)
    {
        var companyData = JsonSerializer.Deserialize<CompanyTaxpayer>(JsonSerializer.Serialize(companyObject));
        if (companyData is null) return [];
        var validProps = new List<string>();
        if (!string.IsNullOrWhiteSpace(companyData.FullName)) validProps.Add(nameof(companyData.FullName));
        if (!string.IsNullOrWhiteSpace(companyData.ShortName)) validProps.Add(nameof(companyData.ShortName));
        if (companyData.NIP?.Length is 10) validProps.Add(nameof(companyData.ShortName));

        return validProps;
    }
    
    private List<string> ValidateIndividualTaxpayer(object? individualObject)
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
    
    private List<string> ValidateAddress(Address address)
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

