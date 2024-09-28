namespace TaxAssistant.Extensions;

public static class FileExtensions
{
    public static string? GetTextFromFile(string path)
    {
        string outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(outputDirectory, path);

        if (File.Exists(filePath))
            return File.ReadAllText(filePath);
        else
            return null;
    }
}
