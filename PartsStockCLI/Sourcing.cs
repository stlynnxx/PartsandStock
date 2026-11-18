namespace PartsStockCLI;

public class Sourcing
{
    private static readonly string Path_ = JsonStore.DocumentsPath("Sourcing.json");

    public int Main()
    {
        Console.Clear();
        Console.WriteLine("--------");
        Console.WriteLine("-Sourcing Menu-");
        Console.WriteLine("  1) Add Sourcing Location");
        Console.WriteLine("  2) Load Sourcing Location");
        Console.WriteLine("  3) Delete Sourcing Location");
        Console.WriteLine("  4) Review Sourcing Locations");
        Console.WriteLine("  5) Home");
        Console.WriteLine("--------");

        string choice = Read.Text("Choice: ").Trim();
        switch (choice)
        {
            case "1": AddSourcingLocation(); break;
            case "2": LoadSourcingLocation(); break;
            case "3": DeleteSourcingLocation(); break;
            case "4": ReviewSourcingLocations(); break;
            case "5": return 1;
            default: Console.WriteLine("Invalid input."); break;
        }

        return 1;
    }

    private static void AddSourcingLocation()
    {
        Console.Clear();
        SourcingLocation loc = new SourcingLocation
        {
            LocationName    = Read.RequiredText("Location Name: "),
            LocationAddress = Read.Text("Location Address: "),
            LocationCity    = Read.Text("Location City: "),
            LocationState   = Read.Text("Location State: ")
        };

        List<SourcingLocation> locs = JsonStore.Load<SourcingLocation>(Path_);
        locs.Add(loc);
        JsonStore.Save(Path_, locs);
        Console.WriteLine($"Sourcing location '{loc.LocationName}' added.");
    }

    private static void LoadSourcingLocation()
    {
        string term = Read.RequiredText("Enter the Sourcing Location: ");
        List<SourcingLocation> matches = JsonStore.Load<SourcingLocation>(Path_)
            .Where(l => (l.LocationName ?? "").Contains(term, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matches.Count == 0)
        {
            Console.WriteLine("No matches found.");
            return;
        }

        foreach (SourcingLocation loc in matches) PrintLocation(loc);
    }

    private static void DeleteSourcingLocation()
    {
        string term = Read.RequiredText("Name of sourcing location to delete: ");
        List<SourcingLocation> locs = JsonStore.Load<SourcingLocation>(Path_);

        int removed = locs.RemoveAll(l =>
            (l.LocationName ?? "").Equals(term, StringComparison.OrdinalIgnoreCase));

        if (removed == 0)
        {
            Console.WriteLine("No matching location found.");
            return;
        }

        JsonStore.Save(Path_, locs);
        Console.WriteLine($"Deleted {removed} location(s).");
    }

    private static void ReviewSourcingLocations()
    {
        List<SourcingLocation> locs = JsonStore.Load<SourcingLocation>(Path_);
        if (locs.Count == 0)
        {
            Console.WriteLine("No sourcing locations defined.");
            return;
        }

        foreach (SourcingLocation loc in locs) PrintLocation(loc);
    }

    private static void PrintLocation(SourcingLocation loc)
    {
        Console.WriteLine($"Name:    {loc.LocationName}");
        Console.WriteLine($"Address: {loc.LocationAddress}");
        Console.WriteLine($"City:    {loc.LocationCity}");
        Console.WriteLine($"State:   {loc.LocationState}");
        Console.WriteLine("--------");
    }
}

public class SourcingLocation
{
    public string LocationName { get; set; } = "";
    public string LocationAddress { get; set; } = "";
    public string LocationCity { get; set; } = "";
    public string LocationState { get; set; } = "";
}