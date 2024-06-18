using Shopping.Models;
using Shopping.Models.Entities;

namespace Shopping.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductForCategoryAsync(int categoryId);

        Task<Product?> GetProductForCategoryAsync(int categoryId, int id);

        Task<bool> CheckCategoryExists(int categoryId);

        Task AddProductForCategoryAsync(int categoryId, Product product, bool autoSave = false);

        Task DeleteProduct(Product product, bool autoSave = false);

        Task SaveChangesAsync();

        Task<(IEnumerable<Product>, PagingMetadataDTO)> GetAllProductsAsync(string? name, string? query, int pageNumber, int pageSize);
    }
}
