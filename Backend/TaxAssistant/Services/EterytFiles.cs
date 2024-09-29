using System;
using System.Xml.Linq;

namespace TaxAssistant.Services;

public class EterytFiles
{
    public XDocument cities { get; set; }
    public XDocument streets { get; set; }
    public XDocument voidevodeships { get; set; }

    public EterytFiles()
    {
        cities = XDocument.Load("Templates/miejscowosci.xml");
        streets = XDocument.Load("Templates/ulice.xml");
        voidevodeships = XDocument.Load("Templates/wojewodztwa.xml");
    }
}
