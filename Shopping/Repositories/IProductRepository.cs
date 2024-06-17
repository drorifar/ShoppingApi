using Shopping.Entities;

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
    }
}
