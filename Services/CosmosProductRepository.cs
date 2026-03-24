using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using MyMvcApp.Configuration;
using MyMvcApp.Models;

namespace MyMvcApp.Services;

public class CosmosProductRepository : IProductRepository
{
    private readonly Container _container;

    public CosmosProductRepository(IOptions<CosmosDbSettings> settings)
    {
        var cosmosSettings = settings.Value;

        var client = new CosmosClient(
            cosmosSettings.AccountEndpoint,
            cosmosSettings.AccountKey);

        _container = client.GetContainer(
            cosmosSettings.DatabaseName,
            cosmosSettings.ContainerName);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var query = new QueryDefinition("SELECT * FROM c");
        var iterator = _container.GetItemQueryIterator<Product>(query);

        var results = new List<Product>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task<List<Product>> GetByCategoryAsync(string category)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.category = @category")
            .WithParameter("@category", category);

        var iterator = _container.GetItemQueryIterator<Product>(query);

        var results = new List<Product>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.id = @id")
            .WithParameter("@id", id);

        var iterator = _container.GetItemQueryIterator<Product>(query);

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            var product = response.FirstOrDefault();

            if (product != null)
            {
                return product;
            }
        }

        return null;
    }
}   