namespace PartsStockCLI;

public class AddItem
{
    private static readonly string Path_ = JsonStore.DocumentsPath("ItemList.json");

    public void NewItem()
    {
        Console.Clear();
        Console.WriteLine("--------");
        Console.WriteLine("New Item");
        Console.WriteLine("--------");

        Item item = new Item
        {
            ItemName          = Read.RequiredText("Item Name: "),
            ItemDescription   = Read.Text("Item Description: "),
            ItemPrice         = Read.Decimal("Item Price: "),
            ItemQuantity      = Read.Int("Item Quantity: ", 1),
            ItemPurchaseDate  = Read.PastOrTodayDate("Item Purchase Date: "),
            StorageLocation   = Read.Text("Storage Location (optional): "),
            SourcingLocation  = Read.Text("Sourcing Location (optional): ")
        };

        List<Item> items = JsonStore.Load<Item>(Path_);
        item.ItemNumber = items.Count == 0 ? 1 : items.Max(i => i.ItemNumber) + 1;
        items.Add(item);
        JsonStore.Save(Path_, items);

        Console.WriteLine();
        Console.WriteLine($"Item Added! (Item Number {item.ItemNumber})");
    }
}
