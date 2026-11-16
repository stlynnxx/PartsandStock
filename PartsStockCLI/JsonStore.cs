using System.Text.Json;
namespace PartsStockCLI;
public static class JsonStore
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };
    public static string DocumentsPath(string fileName) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
    public static List<T> Load<T>(string path)
    {
        if (!File.Exists(path)) { File.WriteAllText(path, "[]"); return new List<T>(); }
        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json)) return new List<T>();
        try { return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>(); }
        catch (JsonException exception)
        {
            throw new InvalidDataException($"The inventory file '{Path.GetFileName(path)}' contains invalid JSON. The original file was left unchanged.", exception);
        }
    }
    public static void Save<T>(string path, List<T> items) => File.WriteAllText(path, JsonSerializer.Serialize(items, Options) + Environment.NewLine);
}