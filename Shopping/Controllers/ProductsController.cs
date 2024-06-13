using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using System.Net.WebSockets;

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

        [HttpPut("{productId}")]
        public ActionResult UpdateProduct(int categoryID, int productId, ProductForUpdateDTO productToUpdate)
        {
            var category = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID);

            if (category == null)
            {
                return NotFound("category not found");
            }
            var product = category.Products.FirstOrDefault(p => p.ID == productId);

            if (product == null)
            {
                return NotFound("product not found");
            }

            if (productToUpdate != null)
            {
                product.Name = productToUpdate.Name;
                product.Description = productToUpdate.Description;


                return NoContent(); // for return success with no object to return
                // return Ok(product); -- there is no reason for returning the same object that the client send us
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="productId"></param>
        /// <param name="patchDocument">
        /// for example:
        /// [  
        ////    {
        ////    "op": "replace",
        ////    "path": "/Name",    
        ////    "value": "Patched  "
        ////  }, 
        ////     {
        ////    "op": "remove",
        ////    "path": "/Description"
        ////  }
        ////]
        /// </param>
        /// <returns></returns>

        [HttpPatch("{productId}")]
        public ActionResult PatchProduct(int categoryID, int productId, JsonPatchDocument<ProductForUpdateDTO> patchDocument)
        {
            var category = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID);

            if (category == null)
            {
                return NotFound("category not found");
            }
            var product = category.Products.FirstOrDefault(p => p.ID == productId);

            if (product == null)
            {
                return NotFound("product not found");
            }


            // we create temp object for patching (because we get from the client a slim object (productForUpdate))
            var productToUpdate = new ProductForUpdateDTO()
            {
                Name = product.Name,
                Description = product.Description
            };

            //running the patch command that we get from the client - updating the productToUpdate from the patchDocument values from the client
            // ModelState - the object from ControllerBase that checks the validation of the given object
            patchDocument.ApplyTo(productToUpdate, ModelState);

            //check that the fields for update in the patchDocument are valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //now we run the validation attrivute on the products to update
            if (!TryValidateModel(productToUpdate))
            {
                return BadRequest(ModelState);
            }

            // when the two checks complete succesfully we update our DB
            product.Name = productToUpdate.Name;
            product.Description = productToUpdate.Description; 

            return NoContent();
        }
    }
}
