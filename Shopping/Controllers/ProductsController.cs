using Microsoft.AspNetCore.Mvc;
using Shopping.Models;

namespace Shopping.Controllers
{
    [ApiController]
    [Route("api/categories/{categoryID}/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts(int categoryID)
        {
            var products = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID)?.Products;

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
    
        }

        [HttpGet("productID", Name = "GetSingleProduct")]
        public ActionResult<IEnumerable<ProductDTO>> GetProduct(int categoryID, int productID)
        {
            var product = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID)?.Products.FirstOrDefault(p => p.ID == productID);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);

        }

        [HttpPost]
        public ActionResult CreateProduct(int categoryID, ProductForCreationDTO productToCreate) //[FromBody] is not neccecery anymore
        {
            //MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID)?.Products.Add(productToCreate);


            var category = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID);

            if (category == null)
            {
                return NotFound();
            }

            var maxProductId = MyDataStore.Current.Categories.SelectMany(c => c.Products).Max(c => c.ID);

            if (productToCreate != null)
            {
                ProductDTO productDTO = new ProductDTO()
                {
                    ID = maxProductId + 1,
                    Name = productToCreate.Name,    
                    Description = productToCreate.Description
                };
                
                category.Products.Add(productDTO);

                return CreatedAtRoute("GetSingleProduct",  //we return (in the header) an object that describe what the route and the params for getting the item that was added - route name, route params)
                    new
                    {
                        categoryID,
                        ProductID = productDTO.ID,
                    }, 
                    productDTO);
            }

            return BadRequest();

            
        }
    }
}
