namespace TaxAssistant.Extensions;

public static class FileExtensions
{
    public static string? GetTextFromFile(string path)
    {
        var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(outputDirectory, path);

        return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
    }
}
