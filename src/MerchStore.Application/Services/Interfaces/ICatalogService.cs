using MerchStore.Domain.Entities;

namespace MerchStore.Application.Services.Interfaces;

public interface ICatalogService
{
    // Gets all available products
    Task<IEnumerable<Product>> GetAllProductsAsync();

    // Gets a product by its unique identifier
    Task<Product?> GetProductByIdAsync(Guid id);
}