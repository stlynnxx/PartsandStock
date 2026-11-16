namespace PartsStockCLI;
public class ItemSearch
{
    private readonly InventoryService _inventory;
    public ItemSearch() : this(new InventoryService(JsonStore.DocumentsPath("ItemList.json"))) { }
    public ItemSearch(InventoryService inventory) => _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
    public void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("What parameter would you like to search by?");
            Console.WriteLine("1) Item Name");
            Console.WriteLine("2) Item Number");
            Console.WriteLine("3) Storage Location");
            Console.WriteLine("4) Sourcing Location");
            Console.WriteLine("5) Back to Main Menu");
            switch (Read.Text("Choice: "))
            {
                case "1": DisplayResults(_inventory.SearchByName(Read.Text("Enter item name (or partial): ")), "Item Name"); break;
                case "2":
                    Item? item = _inventory.GetByNumber(Read.Int("Enter item number: "));
                    Console.WriteLine(item is null ? "No item found with that number." : $"\n--- FOUND ITEM ---\n{item}\n------------------");
                    break;
                case "3": DisplayResults(_inventory.SearchByStorageLocation(Read.Text("Enter storage location name: ")), "Storage Location"); break;
                case "4": DisplayResults(_inventory.SearchBySourcingLocation(Read.Text("Enter sourcing location name: ")), "Sourcing Location"); break;
                case "5": return;
                default: Console.WriteLine("Invalid option."); break;
            }
            Console.WriteLine("\nPress Enter to continue...");
            Read.Text("");
        }
    }
    private static void DisplayResults(IReadOnlyList<Item> matches, string searchType)
    {
        Console.WriteLine($"\n=== Results for {searchType} Search ===\n");
        if (matches.Count == 0) { Console.WriteLine("No items found matching your criteria."); return; }
        Console.WriteLine($"Found {matches.Count} item(s):\n");
        foreach (Item item in matches) { Console.WriteLine("-----------------------------------"); Console.WriteLine(item); }
        Console.WriteLine("-----------------------------------");
    }
}