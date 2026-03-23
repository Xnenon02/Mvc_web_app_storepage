using MyMvcApp.Models;

namespace MyMvcApp.Services;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetByCategoryAsync(string category);
    Task<Product?> GetByIdAsync(string id);
}