using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shopping.Context;
using Shopping.Entities;

namespace Shopping.Repositories
{
    public class ProductRepository(MyDBContext _db) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetProductForCategoryAsync(int categoryId)
        {
            return await _db.Products
                .Where(p => p.CategoryID == categoryId)
                .ToListAsync();
        }
        public async Task<Product?> GetProductForCategoryAsync(int categoryId, int id)
        {
            //return await _db.Products
            //    .Where(p => p.CategoryID == categoryId && p.ID == id)
            //    .FirstOrDefaultAsync();

            return await _db.Products
              .FirstOrDefaultAsync(p => p.CategoryID == categoryId && p.ID == id);
        }

        public async Task<bool> CheckCategoryExists(int categoryId)
        {
            return await _db.Categories.AnyAsync(c => c.ID == categoryId);
        }

        public async Task AddProductForCategoryAsync(int categoryId, Product product, bool autoSave = false)
        {
            Category? category = await _db.Categories
                .FirstOrDefaultAsync(c => c.ID == categoryId);
            if (category != null)
            {
                //product.CategoryID = categoryId;
                category.Products.Add(product);

                if (autoSave)
                {
                    await this.SaveChangesAsync();
                }
            }
        }
        public async Task DeleteProduct(Product product, bool autoSave = false)
        {
            _db.Products.Remove(product);

            if (autoSave)
            {
                await this.SaveChangesAsync();
            }
        }


        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
