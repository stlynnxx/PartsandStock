using System.Text.Json.Serialization;

namespace PartsStockCLI;

public class Item
{
    public string ItemName { get; set; } = "";
    public long ItemNumber { get; set; }
    public string ItemDescription { get; set; } = "";
    public decimal ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public int ItemStock { get; set; }
    public string ItemPurchaseDate { get; set; } = "";
    public string StorageLocation { get; set; } = "";
    public string SourcingLocation { get; set; } = "";  // ← CRITICAL FOR SEARCH

    public override string ToString()
    {
        return $"Item Name: {ItemName} | Item Number: {ItemNumber:D9}\n\"" +
               $"Description: {ItemDescription} | Price: ${ItemPrice}\n" +
               $"Quantity: {ItemQuantity} | Stock: {ItemStock}\n" +
               $"Purchase Date: {ItemPurchaseDate}\n" +
               $"Storage: {StorageLocation} | Sourced From: {SourcingLocation}";
    }
}


public class StorageLocation
{
    public int? Id { get; set; }
    public int? ParentId { get; set; }
    public string LocationName { get; set; } = "";
    public string Details { get; set; } = "";
    public string LocationCity { get; set; } = "";
    public string LocationType { get; set; } = "";

    [JsonIgnore]
    public StorageLocation? Parent { get; set; }

    public List<StorageLocation> Children { get; set; } = new();
}