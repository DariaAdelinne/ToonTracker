// Author: Echipa ToonTracker
// Functionalitate: Repository generic ce salveaza date in fisiere JSON.
using System.Text.Json;

namespace ToonTracker.Data;

public class JsonRepository<T> : IRepository<T>
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public JsonRepository(string filePath)
    {
        _filePath = filePath;
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath) ?? ".");
    }

    public IReadOnlyList<T> GetAll()
    {
        try
        {
            if (!File.Exists(_filePath)) return new List<T>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Fisierul de date este corupt sau are format invalid.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("Datele nu au putut fi citite de pe disc.", ex);
        }
    }

    public void SaveAll(IEnumerable<T> items)
    {
        try
        {
            var json = JsonSerializer.Serialize(items, _options);
            File.WriteAllText(_filePath, json);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("Datele nu au putut fi salvate pe disc.", ex);
        }
    }
}
