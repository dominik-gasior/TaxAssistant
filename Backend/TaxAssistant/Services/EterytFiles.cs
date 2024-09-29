using System.Text.Json;
using System.Xml.Linq;
using TaxAssistant.Extensions;

namespace TaxAssistant.Services;

public class EterytFiles
{
    public XDocument cities { get; set; }
    public XDocument streets { get; set; }
    public XDocument voidevodeships { get; set; }
    public List<string> officies { get; set; }

    public EterytFiles()
    {
        cities = XDocument.Load("Templates/miejscowosci.xml");
        streets = XDocument.Load("Templates/ulice.xml");
        voidevodeships = XDocument.Load("Templates/wojewodztwa.xml");
        var json = FileExtensions.GetTextFromFile("Templates/offices.json");
        officies = JsonSerializer.Deserialize<List<string>>(json!)!;
    }
}
