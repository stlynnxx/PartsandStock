namespace PartsStockCLI;

public class ItemSearch
{
    private static readonly string ItemsPath = JsonStore.DocumentsPath("ItemList.json");

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

            string userParameter = Read.Text("Choice: ");

            switch (userParameter)
            {
                case "1":
                    SearchByName();
                    break;
                case "2":
                    SearchByNumber();
                    break;
                case "3":
                    SearchByStorageLocation();
                    break;
                case "4":
                    SearchBySourcingLocation();
                    break;
                case "5":
                    return; // Simply return to let Route.Routed() loop
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Read.Text("");
        }
    }

    private void SearchByName()
    {
        string searchQuery = Read.Text("Enter item name (or partial): ");
        var items = JsonStore.Load<Item>(ItemsPath);
        var matches = items.Where(i =>
            i.ItemName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

        DisplayResults(matches, "Item Name");
    }

    private void SearchByNumber()
    {
        int itemNum = Read.Int("Enter item number: ");
        var items = JsonStore.Load<Item>(ItemsPath);
        var match = items.FirstOrDefault(i => i.ItemNumber == itemNum);

        if (match != null)
        {
            Console.WriteLine($"\n--- FOUND ITEM ---\n{match}\n------------------");
        }
        else
        {
            Console.WriteLine("No item found with that number.");
        }
    }

    private void SearchByStorageLocation()
    {
        string searchQuery = Read.Text("Enter storage location name: ");
        var items = JsonStore.Load<Item>(ItemsPath);
        var matches = items.Where(i =>
            !string.IsNullOrEmpty(i.StorageLocation) &&
            i.StorageLocation.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

        DisplayResults(matches, "Storage Location");
    }

    private void SearchBySourcingLocation()
    {
        string searchQuery = Read.Text("Enter sourcing location name: ");
        var items = JsonStore.Load<Item>(ItemsPath);
        var matches = items.Where(i =>
            !string.IsNullOrEmpty(i.SourcingLocation) &&
            i.SourcingLocation.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

        DisplayResults(matches, "Sourcing Location");
    }

    private void DisplayResults(List<Item> matches, string searchType)
    {
        Console.WriteLine($"\n=== Results for {searchType} Search ===\n");

        if (matches.Count == 0)
        {
            Console.WriteLine("No items found matching your criteria.");
            return;
        }

        Console.WriteLine($"Found {matches.Count} item(s):\n");

        foreach (var item in matches)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(item.ToString());
        }
        Console.WriteLine("-----------------------------------");
    }
}