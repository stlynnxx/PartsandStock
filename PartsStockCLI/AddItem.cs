namespace PartsStockCLI;
public class AddItem
{
    private readonly InventoryService _inventory;
    public AddItem() : this(new InventoryService(JsonStore.DocumentsPath("ItemList.json"))) { }
    public AddItem(InventoryService inventory) => _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
    public void NewItem()
    {
        Console.Clear();
        Console.WriteLine("--------");
        Console.WriteLine("New Item");
        Console.WriteLine("--------");
        Item item = new()
        {
            ItemName = Read.RequiredText("Item Name: "),
            ItemDescription = Read.Text("Item Description: "),
            ItemPrice = Read.Decimal("Item Price: "),
            ItemQuantity = Read.Int("Item Quantity: ", 1),
            ItemPurchaseDate = Read.PastOrTodayDate("Item Purchase Date: "),
            StorageLocation = Read.Text("Storage Location (optional): "),
            SourcingLocation = Read.Text("Sourcing Location (optional): ")
        };
        _inventory.Create(item);
        Console.WriteLine();
        Console.WriteLine($"Item Added! (Item Number {item.ItemNumber})");
    }
}