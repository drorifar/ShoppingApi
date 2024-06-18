using Microsoft.EntityFrameworkCore;
using Shopping.Context;
using Shopping.Models.Entities;
using SQLitePCL;

namespace Shopping.Repositories
{
    public class CategoryRepository(MyDBContext _db): ICategoryRepository //inject the context 
    {       
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _db.Categories.OrderBy(c => c.Name).ToListAsync(); //execute the query from the DB
        }

        public async Task<Category?> GetCategoryAsync(int categoryId, bool includeProducts)
        {
            if (includeProducts)
            {
                return await _db.Categories
                    .Include(c => c.Products) // add the join to the sql query
                    .Where(c => c.ID == categoryId).FirstOrDefaultAsync();
            }
            return await _db.Categories                 
                 .Where(c => c.ID == categoryId).FirstOrDefaultAsync();

        }
    }
}
