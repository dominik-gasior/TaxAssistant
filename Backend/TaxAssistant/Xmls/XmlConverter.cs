using System.Xml.Serialization;

namespace TaxAssistant.Xmls;

public static class XmlConverter
{
    public static string ConvertObjectToXml<T>(T obj)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        var xmlSerializer = new XmlSerializer(typeof(T));

        using var textWriter = new StringWriter();
        
        xmlSerializer.Serialize(textWriter, obj);
        
        return textWriter.ToString();
    }
}