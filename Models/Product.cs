namespace MyMvcApp.Models;

public class Product
{
    public string Id { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty; // Kan användas som partition key

    public string Name { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ShortDescription { get; set; } = string.Empty;

    public string LongDescription { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int StockQuantity { get; set; }

    public bool IsFeatured { get; set; }

    public ProductSpecs Specs { get; set; } = new();
}