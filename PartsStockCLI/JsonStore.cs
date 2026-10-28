using System.Text.Json;

namespace PartsStockCLI;

public static class JsonStore
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public static string DocumentsPath(string fileName)
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            fileName);
    }

    public static List<T> Load<T>(string path)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "[]");
            return new List<T>();
        }

        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
        }
        catch (JsonException)
        {
            Console.WriteLine($"Warning: {Path.GetFileName(path)} is not valid JSON. Starting empty.");
            return new List<T>();
        }
    }

    public static void Save<T>(string path, List<T> items)
    {
        string json = JsonSerializer.Serialize(items, Options);
        File.WriteAllText(path, json + Environment.NewLine);
    }
}