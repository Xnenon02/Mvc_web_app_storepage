using MyMvcApp.Models;

namespace MyMvcApp.Data;

public static class ProductData
{
    public static List<Product> Products => new()
    {
        new Product
        {
            Id = 1,
            Name = "Gaming Beast X1",
            Category = "Desktop",
            Brand = "ASUS",
            Price = 18999,
            Description = "Kraftfull gaming desktop med hög prestanda för moderna spel.",
            ImageUrl = "/images/desktops/desktop1.jpg"
        },
        new Product
        {
            Id = 2,
            Name = "Office Pro Desk",
            Category = "Desktop",
            Brand = "HP",
            Price = 10999,
            Description = "Stabil stationär dator för arbete, studier och vardagligt bruk.",
            ImageUrl = "/images/desktops/desktop2.jpg"
        },
        new Product
        {
            Id = 3,
            Name = "Creator Tower Z",
            Category = "Desktop",
            Brand = "Lenovo",
            Price = 15999,
            Description = "Desktop för content creation, redigering och multitasking.",
            ImageUrl = "/images/desktops/desktop3.jpg"
        },
        new Product
        {
            Id = 4,
            Name = "SlimBook 14",
            Category = "Laptop",
            Brand = "Acer",
            Price = 9999,
            Description = "Lätt och smidig laptop för studenter och kontorsarbete.",
            ImageUrl = "/images/laptops/laptop1.jpg"
        },
        new Product
        {
            Id = 5,
            Name = "PowerNote G5",
            Category = "Laptop",
            Brand = "MSI",
            Price = 14999,
            Description = "Gaming laptop med stark processor och dedikerat grafikkort.",
            ImageUrl = "/images/laptops/laptop2.jpg"
        },
        new Product
        {
            Id = 6,
            Name = "Business Air 15",
            Category = "Laptop",
            Brand = "Dell",
            Price = 12999,
            Description = "Pålitlig laptop för arbete, möten och produktivitet.",
            ImageUrl = "/images/laptops/laptop3.jpg"
        }
    };
}