using PartsStockCLI;
using Xunit;
namespace PartsStockCLI.Tests;
public sealed class InventoryServiceTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
    private readonly string _path;
    public InventoryServiceTests() { Directory.CreateDirectory(_directory); _path = Path.Combine(_directory, "ItemList.json"); }

    [Fact]
    public void Create_AssignsSequentialNumbersAndPersists()
    {
        var inventory = new InventoryService(_path);
        Assert.Equal(1, inventory.Create(NewItem("Resistor")).ItemNumber);
        Assert.Equal(2, inventory.Create(NewItem("Capacitor")).ItemNumber);
        Assert.Equal(2, new InventoryService(_path).GetAll().Count);
    }

    [Fact]
    public void GetByNumber_ReturnsItemOrNull()
    {
        var inventory = new InventoryService(_path);
        var item = inventory.Create(NewItem("Controller"));
        Assert.Equal(item.ItemName, inventory.GetByNumber(item.ItemNumber)?.ItemName);
        Assert.Null(inventory.GetByNumber(999));
    }

    [Fact]
    public void Update_UpdatesExistingItemAndRejectsUnknownNumber()
    {
        var inventory = new InventoryService(_path);
        var item = inventory.Create(NewItem("Old name"));
        item.ItemName = "Updated name";
        item.ItemQuantity = 12;
        Assert.True(inventory.Update(item));
        Assert.Equal("Updated name", inventory.GetByNumber(item.ItemNumber)?.ItemName);
        Assert.Equal(12, inventory.GetByNumber(item.ItemNumber)?.ItemQuantity);
        var missing = NewItem("Missing");
        missing.ItemNumber = 999;
        Assert.False(inventory.Update(missing));
    }

    [Fact]
    public void Delete_RemovesOnlyRequestedItem()
    {
        var inventory = new InventoryService(_path);
        var retained = inventory.Create(NewItem("Retained"));
        var removed = inventory.Create(NewItem("Removed"));
        Assert.True(inventory.Delete(removed.ItemNumber));
        Assert.Equal(retained.ItemNumber, Assert.Single(inventory.GetAll()).ItemNumber);
        Assert.False(inventory.Delete(removed.ItemNumber));
    }

    [Fact]
    public void Search_IsPartialAndCaseInsensitiveAcrossNameAndLocations()
    {
        var inventory = new InventoryService(_path);
        var item = NewItem("Precision Resistor");
        item.StorageLocation = "Workshop Shelf A";
        item.SourcingLocation = "Mumbai Electronics";
        inventory.Create(item);
        Assert.Equal("Precision Resistor", Assert.Single(inventory.SearchByName("RESIST")).ItemName);
        Assert.Single(inventory.SearchByStorageLocation("shelf"));
        Assert.Single(inventory.SearchBySourcingLocation("MUMBAI"));
        Assert.Empty(inventory.SearchByStorageLocation("garage"));
    }

    [Fact]
    public void MissingFile_CreatesEmptyInventoryInTemporaryDirectory()
    {
        Assert.Empty(new InventoryService(_path).GetAll());
        Assert.True(File.Exists(_path));
        Assert.Equal("[]", File.ReadAllText(_path).Trim());
    }

    [Fact]
    public void InvalidJson_ThrowsAndPreservesOriginalFile()
    {
        const string invalid = "{ invalid inventory data";
        File.WriteAllText(_path, invalid);
        Assert.Throws<InvalidDataException>(() => new InventoryService(_path).GetAll());
        Assert.Equal(invalid, File.ReadAllText(_path));
    }

    [Fact]
    public void NullArguments_AreRejected()
    {
        var inventory = new InventoryService(_path);
        Assert.Throws<ArgumentNullException>(() => inventory.Create(null!));
        Assert.Throws<ArgumentNullException>(() => inventory.Update(null!));
        Assert.Throws<ArgumentNullException>(() => inventory.SearchByName(null!));
    }

    private static Item NewItem(string name) => new()
    {
        ItemName = name, ItemDescription = "Test item", ItemPrice = 1.25m,
        ItemQuantity = 1, ItemPurchaseDate = "2026-01-01"
    };
    public void Dispose() { if (Directory.Exists(_directory)) Directory.Delete(_directory, true); }
}