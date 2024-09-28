using Scriban;
using TaxAssistant.Utils;
using TaxAssistant.Utils.Exceptions;

namespace TaxAssistant.Models;

public interface IFormService
{
    public string Generate(string formName, object replaceGap);
}

public class FormService : IFormService
{
    public string Generate(string formName, object replaceGap)
    {
        var form = FetchForm(formName);
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
