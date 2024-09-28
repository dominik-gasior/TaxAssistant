using Scriban;
using TaxAssistant.Extensions;

namespace TaxAssistant.Services;

public interface IFormService
{
    public string Generate(string templateName, object replaceGap);
}

public class FormParser : IFormService
{
    public string Generate(string templateName, object replaceGap)
    {
        var text = FileExtensions.GetTextFromFile(templateName);
        var template = Template.Parse(text);
        return template.Render(replaceGap);
    }
}
