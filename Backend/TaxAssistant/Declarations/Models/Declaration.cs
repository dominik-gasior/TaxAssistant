using System.Xml.Serialization;

namespace TaxAssistant.Declarations.Models;

[XmlRoot(ElementName = "Deklaracja", Namespace = "http://crd.gov.pl/wzor/2023/12/13/13064/")]
public class Declaration
{
    [XmlElement(ElementName = "Naglowek")]
    public Header Header { get; set; }

    [XmlElement(ElementName = "Podmiot1")]
    public Entity1 Entity1 { get; set; }

    [XmlElement(ElementName = "PozycjeSzczegolowe")]
    public DetailedPositions DetailedPositions { get; set; }

    [XmlElement(ElementName = "Pouczenia")]
    public int Instructions { get; set; }
}

public class Header
{
    [XmlElement(ElementName = "KodFormularza")]
    public FormCode FormCode { get; set; }

    [XmlElement(ElementName = "WariantFormularza")]
    public int FormVariant { get; set; }

    [XmlElement(ElementName = "CelZlozenia")]
    public int SubmissionPurpose { get; set; }

    [XmlElement(ElementName = "Data")]
    public DateTime Date { get; set; }

    [XmlElement(ElementName = "KodUrzedu")]
    public int OfficeCode { get; set; }
}

public class FormCode
{
    [XmlAttribute(AttributeName = "KodSystemowy")]
    public string SystemCode { get; set; }

    [XmlAttribute(AttributeName = "KodPodatku")]
    public string TaxCode { get; set; }

    [XmlAttribute(AttributeName = "RodzajZobowiazania")]
    public string ObligationType { get; set; }

    [XmlAttribute(AttributeName = "WersjaSchemy")]
    public string SchemaVersion { get; set; }

    [XmlText]
    public string Value { get; set; }
}

public class Entity1
{
    [XmlElement(ElementName = "OsobaFizyczna")]
    public Individual Individual { get; set; }

    [XmlElement(ElementName = "AdresZamieszkaniaSiedziby")]
    public Address Address { get; set; }
}

public class Individual
{
    [XmlElement(ElementName = "PESEL")]
    public string PESEL { get; set; }

    [XmlElement(ElementName = "ImiePierwsze")]
    public string FirstName { get; set; }

    [XmlElement(ElementName = "Nazwisko")]
    public string LastName { get; set; }

    [XmlElement(ElementName = "DataUrodzenia")]
    public DateTime DateOfBirth { get; set; }
}

public class Address
{
    [XmlAttribute(AttributeName = "RodzajAdresu")]
    public string AddressType { get; set; }

    [XmlElement(ElementName = "AdresPol")]
    public PolishAddress PolishAddress { get; set; }
}

public class PolishAddress
{
    [XmlElement(ElementName = "KodKraju")]
    public string CountryCode { get; set; }

    [XmlElement(ElementName = "Wojewodztwo")]
    public string Province { get; set; }

    [XmlElement(ElementName = "Powiat")]
    public string District { get; set; }

    [XmlElement(ElementName = "Gmina")]
    public string Municipality { get; set; }

    [XmlElement(ElementName = "Ulica")]
    public string Street { get; set; }

    [XmlElement(ElementName = "NrDomu")]
    public string HouseNumber { get; set; }

    [XmlElement(ElementName = "NrLokalu")]
    public string ApartmentNumber { get; set; }

    [XmlElement(ElementName = "Miejscowosc")]
    public string City { get; set; }

    [XmlElement(ElementName = "KodPocztowy")]
    public string PostalCode { get; set; }
}

public class DetailedPositions
{
    [XmlElement(ElementName = "P_7")]
    public int P7 { get; set; }

    [XmlElement(ElementName = "P_20")]
    public int P20 { get; set; }

    [XmlElement(ElementName = "P_21")]
    public int P21 { get; set; }

    [XmlElement(ElementName = "P_22")]
    public int P22 { get; set; }

    [XmlElement(ElementName = "P_23")]
    public string P23 { get; set; }

    [XmlElement(ElementName = "P_24")]
    public decimal P24 { get; set; }

    [XmlElement(ElementName = "P_25")]
    public decimal P25 { get; set; }

    [XmlElement(ElementName = "P_46")]
    public decimal P46 { get; set; }

    [XmlElement(ElementName = "P_53")]
    public decimal P53 { get; set; }

    [XmlElement(ElementName = "P_62")]
    public int P62 { get; set; }
}