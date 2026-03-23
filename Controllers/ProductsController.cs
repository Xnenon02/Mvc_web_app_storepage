using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers;

public class ProductsController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Desktop()
    {
        var desktops = await _productRepository.GetByCategoryAsync("Desktop");
        return View(desktops);
    }

    public async Task<IActionResult> Laptops()
    {
        var laptops = await _productRepository.GetByCategoryAsync("Laptop");
        return View(laptops);
    }

    public async Task<IActionResult> Details(string id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}