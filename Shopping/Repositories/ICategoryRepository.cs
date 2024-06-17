using Shopping.Context;
using Shopping.Entities;

namespace Shopping.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category?> GetCategoryAsync(int categoryId, bool includeProducts);


    }
}
