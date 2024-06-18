using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shopping.Context;
using Shopping.Models;
using Shopping.Models.Entities;
using System.Linq;
using System.Xml.Linq;

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

        public async Task<(IEnumerable<Product>, PagingMetadataDTO)> GetAllProductsAsync(string? name, string? query, int pageNumber, int pageSize)
        {
            // we build the query according to the param (exect name or query)

            IQueryable<Product> collection = _db.Products as IQueryable<Product>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(p => p.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.Trim();
                collection = collection.Where(p => p.Name.Contains(query) || (p.Description != null && p.Description.Contains(query))); 
            }

            int count = collection.Count();

            PagingMetadataDTO meta = new Models.PagingMetadataDTO()
            {
                TotalItemCount = count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            collection = collection.OrderBy(p => p.Name);

            collection = collection.Skip((pageNumber - 1)*pageSize).Take(pageSize); // skip and take add the paging to the db query

            var items = await collection.ToListAsync();
            //only in the end we execute the query to the DB
            return (items, meta);
        }

        //NOT RECOMENDED - exemple for returning the query before executions and now the controller can get excess to the DB qury
        public IQueryable<Product> GetProducstQuery()
        {
            return _db.Products;
        }
    }
}
