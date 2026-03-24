using MyMvcApp.Models;

namespace MyMvcApp.Services;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product
        {
            Id = "desktop-001",
            Name = "Gaming Beast X1",
            Category = "Desktop",
            Brand = "ASUS",
            Price = 18999,
            ShortDescription = "Kraftfull gaming desktop.",
            LongDescription = "En kraftfull gaming desktop med hög prestanda för moderna spel, streaming och multitasking.",
            ImageUrl = "/images/desktops/desktop1.jpg",
            StockQuantity = 8,
            IsFeatured = true,
            Specs = new ProductSpecs
            {
                Cpu = "Intel Core i7",
                Gpu = "NVIDIA RTX 4070",
                Ram = "32 GB DDR5",
                Storage = "1 TB SSD",
                FormFactor = "Mid Tower",
                OperatingSystem = "Windows 11"
            }
        },
        new Product
        {
            Id = "desktop-002",
            Name = "Office Pro Desk",
            Category = "Desktop",
            Brand = "HP",
            Price = 10999,
            ShortDescription = "Stabil desktop för kontor och studier.",
            LongDescription = "Pålitlig stationär dator som passar perfekt för kontorsarbete, studier och vardaglig användning.",
            ImageUrl = "/images/desktops/desktop2.jpg",
            StockQuantity = 12,
            IsFeatured = false,
            Specs = new ProductSpecs
            {
                Cpu = "Intel Core i5",
                Gpu = "Intel UHD Graphics",
                Ram = "16 GB DDR4",
                Storage = "512 GB SSD",
                FormFactor = "Mini Tower",
                OperatingSystem = "Windows 11"
            }
        },
        new Product
        {
            Id = "desktop-003",
            Name = "Creator Tower Z",
            Category = "Desktop",
            Brand = "Lenovo",
            Price = 15999,
            ShortDescription = "Desktop för content creation.",
            LongDescription = "Byggd för videoredigering, bildhantering och produktivt arbete med flera program samtidigt.",
            ImageUrl = "/images/desktops/desktop3.jpg",
            StockQuantity = 5,
            IsFeatured = true,
            Specs = new ProductSpecs
            {
                Cpu = "AMD Ryzen 7",
                Gpu = "NVIDIA RTX 4060",
                Ram = "32 GB DDR5",
                Storage = "1 TB NVMe SSD",
                FormFactor = "Mid Tower",
                OperatingSystem = "Windows 11 Pro"
            }


        },
        new Product
        {
            Id = "desktop-004",
            Name = "Creator Tower x",
            Category = "Desktop",
            Brand = "Lenovo",
            Price = 1599900000,
            ShortDescription = "Desktop för DA RICH",
            LongDescription = "Byggd för videoredigering, bildhantering och produktivt arbete med flera program samtidigt.",
            ImageUrl = "/images/desktops/desktop4.jpg",
            StockQuantity = 1,
            IsFeatured = true,
            Specs = new ProductSpecs
            {
                Cpu = "AMD Ryzen 9",
                Gpu = "NVIDIA RTX 4090",
                Ram = "64 GB DDR5",
                Storage = "4 TB NVMe SSD",
                FormFactor = "Mid Tower",
                OperatingSystem = "Windows 11 Pro"
            }


        },
        new Product
        {
            Id = "laptop-001",
            Name = "SlimBook 14",
            Category = "Laptop",
            Brand = "Acer",
            Price = 9999,
            ShortDescription = "Smidig laptop för studier.",
            LongDescription = "Lätt och tunn laptop som passar bra för studenter, surf och kontorsarbete.",
            ImageUrl = "/images/laptops/laptop1.jpg",
            StockQuantity = 15,
            IsFeatured = true,
            Specs = new ProductSpecs
            {
                Cpu = "Intel Core i5",
                Gpu = "Intel Iris Xe",
                Ram = "16 GB LPDDR5",
                Storage = "512 GB SSD",
                ScreenSize = "14 tum",
                OperatingSystem = "Windows 11"
            }
        },
        new Product
        {
            Id = "laptop-002",
            Name = "PowerNote G5",
            Category = "Laptop",
            Brand = "MSI",
            Price = 14999,
            ShortDescription = "Gaming laptop med stark grafik.",
            LongDescription = "Gaming laptop med hög prestanda för spel, arbete och kreativt skapande.",
            ImageUrl = "/images/laptops/laptop2.jpg",
            StockQuantity = 7,
            IsFeatured = true,
            Specs = new ProductSpecs
            {
                Cpu = "Intel Core i7",
                Gpu = "NVIDIA RTX 4060",
                Ram = "16 GB DDR5",
                Storage = "1 TB SSD",
                ScreenSize = "15.6 tum",
                OperatingSystem = "Windows 11"
            }
        },
        new Product
        {
            Id = "laptop-003",
            Name = "Business Air 15",
            Category = "Laptop",
            Brand = "Dell",
            Price = 12999,
            ShortDescription = "Pålitlig laptop för arbete.",
            LongDescription = "En affärsinriktad laptop för möten, kontor, produktivitet och mobilt arbete.",
            ImageUrl = "/images/laptops/laptop3.jpg",
            StockQuantity = 10,
            IsFeatured = false,
            Specs = new ProductSpecs
            {
                Cpu = "Intel Core i5",
                Gpu = "Intel Iris Xe",
                Ram = "16 GB DDR4",
                Storage = "512 GB SSD",
                ScreenSize = "15.6 tum",
                OperatingSystem = "Windows 11 Pro"
            }
        }
    };

    public Task<List<Product>> GetAllAsync()
    {
        return Task.FromResult(_products.ToList());
    }

    public Task<List<Product>> GetByCategoryAsync(string category)
    {
        var result = _products
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult(result);
    }

    public Task<Product?> GetByIdAsync(string id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }
}