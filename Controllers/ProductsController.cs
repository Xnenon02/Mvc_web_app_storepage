using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class ProductsController : Controller
{
    public IActionResult Desktop()
    {
        var desktops = ProductData.Products
            .Where(p => p.Category == "Desktop")
            .ToList();

        return View(desktops);
    }

    public IActionResult Laptops()
    {
        var laptops = ProductData.Products
            .Where(p => p.Category == "Laptop")
            .ToList();

        return View(laptops);
    }

    public IActionResult Details(int id)
    {
        var product = ProductData.Products
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}