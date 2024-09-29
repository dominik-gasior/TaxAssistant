namespace TaxAssistant.Services;

public interface IEterytService
{
    object GetVoivodeships();
    object GetProvinces(string voivodeshipID);
    object GetMunicipalities(string voivodeshipID, string provinceID);
    object GetCities(string voivodeshipID, string provinceID, string municipalityID);
    object GetStreets(string voivodeshipID, string provinceID, string municipalityID);
    List<string> GetOffices();
    public bool ValidateOffices(string officeId);
}

public class EterytService : IEterytService
{
    private readonly EterytFiles _eterytFiles;

    public EterytService(EterytFiles eterytFiles)
    {
        _eterytFiles = eterytFiles;
    }

    public object GetCities(string voivodeshipID, string provinceID, string municipalityID)
        => _eterytFiles.cities.Root?.Element("catalog").Elements("row")
            .Where(x => x.Element("WOJ").Value == voivodeshipID.ToString()
                        && x.Element("POW").Value == provinceID.ToString()
                        && x.Element("GMI").Value == municipalityID.ToString())
            .Select(x => new
            {
                SYM = x.Element("SYM")?.Value,
                NAZWA = x.Element("NAZWA")?.Value,
            }).Distinct().ToList();

    public object GetMunicipalities(string voivodeshipID, string provinceID)
        => _eterytFiles.voidevodeships.Root?.Element("catalog").Elements("row")
            .Where(x => x.Element("WOJ").Value == voivodeshipID.ToString()
                        && x.Element("POW").Value == provinceID.ToString()
                        && x.Element("GMI").Value != "")
            .Select(x => new
            {
                GMI = x.Element("GMI")?.Value,
                NAZWA = x.Element("NAZWA")?.Value,
            }).Distinct().ToList();

    public object GetProvinces(string voivodeshipID)
        => _eterytFiles.voidevodeships.Root?.Element("catalog").Elements("row")
            .Where(x => x.Element("GMI").Value == ""
                        && x.Element("WOJ").Value == voivodeshipID.ToString()
                        && x.Element("POW").Value != "")
            .Select(x => new
            {
                POW = x.Element("POW")?.Value,
                NAZWA = x.Element("NAZWA")?.Value,
            }).ToList();

    public object GetStreets(string voivodeshipID, string provinceID, string municipalityID)
        => _eterytFiles.streets.Root?.Element("catalog").Elements("row")
            .Where(x => x.Element("WOJ").Value == voivodeshipID.ToString()
                        && x.Element("POW").Value == provinceID.ToString()
                        && x.Element("GMI").Value == municipalityID.ToString())
            .Select(x => new
            {
                SYM = x.Element("SYM")?.Value,
                CECHA = x.Element("CECHA")?.Value,
                NAZWA = x.Element("NAZWA_2")?.Value == "" ?
                 x.Element("NAZWA_1")?.Value :
                  x.Element("NAZWA_2")?.Value + " " + x.Element("NAZWA_1")?.Value,
            }).ToList();

    public object GetVoivodeships()
        => _eterytFiles.voidevodeships.Root?.Element("catalog").Elements("row")
            .Where(x => x.Element("POW").Value == "")
            .Select(x => new
            {
                WOJ = x.Element("WOJ")?.Value,
                NAZWA = x.Element("NAZWA")?.Value,
            }).ToList();

    public List<string> GetOffices() => _eterytFiles.officies;
    public bool ValidateOffices(string officeId) => _eterytFiles.officies.Exists(x => x == officeId);

}
