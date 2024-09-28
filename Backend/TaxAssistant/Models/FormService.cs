using Scriban;
using TaxAssistant.Extensions;
using TaxAssistant.Extensions.Exceptions;

namespace TaxAssistant.Models;

public interface IFormService
{
    public string Generate(string templateName, object replaceGap);
}

public class FormService : IFormService
{
    public string Generate(string templateName, object replaceGap)
    {
        var form = FetchForm(templateName);
        return ReplaceGapsInForm(form, replaceGap);
    }

    private string FetchForm(string formName)
    {
        var text = FileExtensions.GetTextFromFile(formName)
            ?? throw new NotFoundException($"Form {formName} not found");

        return text;
    }

    private string ReplaceGapsInForm(string text, object replaceGap)
    {
        var template = Template.Parse(text);

        return template.Render(replaceGap);
    }
}
