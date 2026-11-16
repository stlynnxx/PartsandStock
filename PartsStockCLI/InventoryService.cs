namespace PartsStockCLI;

public sealed class InventoryService
{
    private readonly string _path;

    public InventoryService(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        _path = path;
    }

    public Item Create(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var items = JsonStore.Load<Item>(_path);
        item.ItemNumber = items.Count == 0 ? 1 : checked(items.Max(x => x.ItemNumber) + 1);
        items.Add(item);
        JsonStore.Save(_path, items);
        return item;
    }

    public IReadOnlyList<Item> GetAll() => JsonStore.Load<Item>(_path);

    public Item? GetByNumber(int number) =>
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

    public bool Delete(int number)
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