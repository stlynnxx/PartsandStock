namespace PartsStockCLI;

public sealed class InventoryService
{
    private readonly string _path;

    public InventoryService(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        _path = path;
        _counterPath = Path.ChangeExtension(path, ".counter");
    }
    private readonly string _counterPath;
    
    public class InventoryData
    {
        public long NextItemNumber { get; set; } = 1;

        public List<Item> Items { get; set; } = new();
    }

    public Item Create(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var items = JsonStore.Load<Item>(_path);
        long highestExisting = items.Count == 0
            ? 0
            : items.Max(x => x.ItemNumber);
        long lastAssigned = highestExisting;

        if (File.Exists(_counterPath))
        {
            string text = File.ReadAllText(_counterPath);
            if (!long.TryParse(text, out long savedNumber) ||
                savedNumber < 0)
            {
                throw new InvalidDataException("Invalid item number counter.");
            }
            lastAssigned = Math.Max(lastAssigned, savedNumber);
        }

        long nextNumber = checked(lastAssigned + 1);
        item.ItemNumber = nextNumber;
        items.Add(item);
        JsonStore.Save(_path, items);
        File.WriteAllText(_counterPath, nextNumber.ToString());

        return item;
    }

    public IReadOnlyList<Item> GetAll() => JsonStore.Load<Item>(_path);

    public Item? GetByNumber(long number) =>
        JsonStore.Load<Item>(_path).FirstOrDefault(x => x.ItemNumber == number);

    public bool Update(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var items = JsonStore.Load<Item>(_path);
        int index = items.FindIndex(x => x.ItemNumber == item.ItemNumber);
        if (index < 0) return false;
        items[index] = item;
        JsonStore.Save(_path, items);
        return true;
    }

    public bool Delete(long number)
    {
        var items = JsonStore.Load<Item>(_path);
        if (items.RemoveAll(x => x.ItemNumber == number) == 0) return false;
        JsonStore.Save(_path, items);
        return true;
    }

    public IReadOnlyList<Item> SearchByName(string term)
    {
        ArgumentNullException.ThrowIfNull(term);
        return JsonStore.Load<Item>(_path)
            .Where(x => x.ItemName.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IReadOnlyList<Item> SearchByStorageLocation(string term)
    {
        ArgumentNullException.ThrowIfNull(term);
        return JsonStore.Load<Item>(_path)
            .Where(x => !string.IsNullOrWhiteSpace(x.StorageLocation) &&
                        x.StorageLocation.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IReadOnlyList<Item> SearchBySourcingLocation(string term)
    {
        ArgumentNullException.ThrowIfNull(term);
        return JsonStore.Load<Item>(_path)
            .Where(x => !string.IsNullOrWhiteSpace(x.SourcingLocation) &&
                        x.SourcingLocation.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    
}