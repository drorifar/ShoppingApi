using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shopping.Context;
using Shopping.Models;
using Shopping.Models.Entities;
using Shopping.Repositories;
using Shopping.services;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Shopping.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/categories/{categoryID}/products")]
    public class ProductsController : ControllerBase
    {
        private ILogger<ProductsController> _logger;
        private IMailService _mailService;
        private IProductRepository _repo;
        private IMapper _mapper;
        readonly int MAX_PAGE_SIZE = 10;

        public ProductsController(ILogger<ProductsController> logger, IMailService mailService, MyDBContext context, IProductRepository repo, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //var log = HttpContext.RequestServices.GetService(typeof(ILogger<ProductsController>)); // anothe way to use the services without the DI (if not availible)            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(int categoryID)
        {
            //var products = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID)?.Products;

            //if (products == null)
            //{
            //    _logger.LogWarning($"No category with id: {categoryID}");
            //    _mailService.send("Get Products", $"no category found: {categoryID}");
            //    return NotFound();
            //}

            //return Ok(products);

            if (!(await _repo.CheckCategoryExists(categoryID)))
            {
                _logger.LogWarning($"No category with id: {categoryID}");
                _mailService.send("Get Products", $"no category found: {categoryID}");
                return NotFound($"No category with id: {categoryID}");
            }

            IEnumerable<Product> products = await _repo.GetProductForCategoryAsync(categoryID);
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));

        }

        [HttpGet("/api/products")] // when the url start with "/" than its start from the root anf not from the controller api
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts(
            [FromQuery]string? name, //[FromQuery] - not needed as default is from query
            string? query,
            int pageNumber = 1,
            int pageSize = 10) 
        {

            if (pageSize > MAX_PAGE_SIZE)
            {
                pageSize = MAX_PAGE_SIZE;
            }

            // not the best policy implementation
            //if (int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "auth_level")?.Value, out int authLevel))
            //{
            //    if (authLevel != 9) // for exemple we can use the claims of the user to check for example its authenticatiom level
            //    {
            //        return Unauthorized();
            //    }
            //}

            var (results, meta) = await _repo.GetAllProductsAsync(name, query, pageNumber, pageSize);

            Response.Headers.Append("X-TotalItemCount", JsonSerializer.Serialize(meta.TotalItemCount.ToString())); // add the metaData to the header
            Response.Headers.Append("X-TotalPageCount", JsonSerializer.Serialize(meta.TotalPageCount.ToString())); // add the metaData to the header
            Response.Headers.Append("X-PageSize", JsonSerializer.Serialize(meta.PageSize.ToString())); // add the metaData to the header
            Response.Headers.Append("X-PageNumber", JsonSerializer.Serialize(meta.PageNumber.ToString())); // add the metaData to the header

            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(results));
        }

        [HttpGet("productID", Name = "GetSingleProduct")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int categoryID, int productID)
        {
            //var product = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID)?.Products.FirstOrDefault(p => p.ID == productID);

            //if (product == null)
            //{
            //    return NotFound();
            //}

            //return Ok(product);

            if (!(await _repo.CheckCategoryExists(categoryID)))
            {
                _logger.LogWarning($"No category with id: {categoryID}");
                _mailService.send("Get Products", $"no category found: {categoryID}");
                return NotFound($"No category with id: {categoryID}");
            }

            Product? product = await _repo.GetProductForCategoryAsync(categoryID, productID);

            if (product == null)
            {
                return NotFound($"No product with id: {productID}");
            }

            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(int categoryID, ProductForCreationDTO productToCreate) //[FromBody] is not neccecery anymore
        {
            //MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID)?.Products.Add(productToCreate);


            //var category = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID);

            //if (category == null)
            //{
            //    return NotFound();
            //}

            //var maxProductId = MyDataStore.Current.Categories.SelectMany(c => c.Products).Max(c => c.ID);

            //if (productToCreate != null)
            //{
            //    ProductDTO productDTO = new ProductDTO()
            //    {
            //        ID = maxProductId + 1,
            //        Name = productToCreate.Name,    
            //        Description = productToCreate.Description
            //    };

            //    category.Products.Add(productDTO);

            //    return CreatedAtRoute("GetSingleProduct",  //we return (in the header) an object that describe what the route and the params for getting the item that was added - route name, route params)
            //        new
            //        {
            //            categoryID,
            //            ProductID = productDTO.ID,
            //        }, 
            //        productDTO);
            //}

            //return BadRequest();

            if (!(await _repo.CheckCategoryExists(categoryID)))
            {
                _logger.LogWarning($"No category with id: {categoryID}");
                _mailService.send("Get Products", $"no category found: {categoryID}");
                return NotFound($"No category with id: {categoryID}");
            }

            var product = _mapper.Map<Product>(productToCreate);

            await _repo.AddProductForCategoryAsync(categoryID, product, true);

            var productDTO =  _mapper.Map<ProductDTO>(product);


            return CreatedAtRoute("GetSingleProduct",  //we return (in the header) an object that describe what the route and the params for getting the item that was added - route name, route params)
                    new
                    {
                        categoryID,
                        ProductID = productDTO.ID,
                    },
                    productDTO);
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateProduct(int categoryID, int productId, ProductForUpdateDTO productToUpdate)
        {
            //var category = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID);

            //if (category == null)
            //{
            //    return NotFound("category not found");
            //}
            //var product = category.Products.FirstOrDefault(p => p.ID == productId);

            //if (product == null)
            //{
            //    return NotFound("product not found");
            //}

            //if (productToUpdate != null)
            //{
            //    product.Name = productToUpdate.Name;
            //    product.Description = productToUpdate.Description;


            //    return NoContent(); // for return success with no object to return
            //    // return Ok(product); -- there is no reason for returning the same object that the client send us
            //}
            //else
            //{
            //    return BadRequest();
            //}


            if (!(await _repo.CheckCategoryExists(categoryID)))
            {
                _logger.LogWarning($"No category with id: {categoryID}");
                _mailService.send("Get Products", $"no category found: {categoryID}");
                return NotFound($"No category with id: {categoryID}");
            }

            Product? product = await _repo.GetProductForCategoryAsync(categoryID, productId);

            if (product == null)
            {
                return NotFound("product not found");
            }

            _mapper.Map(productToUpdate, product); //the mapper updated all the fields that not the same

            await _repo.SaveChangesAsync(); //because we change the product object from db all we need is to save the changes to DB

            return NoContent();

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
        public async Task<ActionResult> PatchProduct(int categoryID, int productId, JsonPatchDocument<ProductForUpdateDTO> patchDocument)
        {

            //var category = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID);

            //if (category == null)
            //{
            //    return NotFound("category not found");
            //}
            //var product = category.Products.FirstOrDefault(p => p.ID == productId);

            //if (product == null)
            //{
            //    return NotFound("product not found");
            //}


            //// we create temp object for patching (because we get from the client a slim object (productForUpdate))
            //var productToUpdate = new ProductForUpdateDTO()
            //{
            //    Name = product.Name,
            //    Description = product.Description
            //};

            ////running the patch command that we get from the client - updating the productToUpdate from the patchDocument values from the client
            //// ModelState - the object from ControllerBase that checks the validation of the given object
            //patchDocument.ApplyTo(productToUpdate, ModelState);

            ////check that the fields for update in the patchDocument are valid
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            ////now we run the validation attrivute on the products to update
            //if (!TryValidateModel(productToUpdate))
            //{
            //    return BadRequest(ModelState);
            //}

            //// when the two checks complete succesfully we update our DB
            //product.Name = productToUpdate.Name;
            //product.Description = productToUpdate.Description;

            //return NoContent();


            if (!(await _repo.CheckCategoryExists(categoryID)))
            {
                _logger.LogWarning($"No category with id: {categoryID}");
                _mailService.send("Get Products", $"no category found: {categoryID}");
                return NotFound($"No category with id: {categoryID}");
            }

            Product? product = await _repo.GetProductForCategoryAsync(categoryID, productId);

            if (product == null)
            {
                return NotFound("product not found");
            }

            ProductForUpdateDTO productForUpdate = _mapper.Map<ProductForUpdateDTO>(product); //map to ProductForUpdateDTO for validation checks and we dont want the patch update our db object

            patchDocument.ApplyTo(productForUpdate, ModelState);

            //check that the fields for update in the patchDocument are valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //now we run the validation attrivute on the products to update
            if (!TryValidateModel(productForUpdate))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(productForUpdate, product); //after the patch we return the changes to product

            await _repo.SaveChangesAsync(); //because we change the product object from db all we need is to save the changes to DB

            return NoContent();


        }

        [HttpDelete("{productId}")]
        [Authorize(Policy = "IsShopAdmin")] // check that the policy in the program is valid
        public async Task<ActionResult> DeleteProduct(int categoryID, int productId)
        {
            //var category = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == categoryID);

            //if (category == null)
            //{
            //    return NotFound("category not found");
            //}
            //var product = category.Products.FirstOrDefault(p => p.ID == productId);

            //if (product == null)
            //{
            //    return NotFound("product not found");
            //}

            //category.Products.Remove(product);

            //return Ok("Deleted item");

            if (!(await _repo.CheckCategoryExists(categoryID)))
            {
                _logger.LogWarning($"No category with id: {categoryID}");
                _mailService.send("Get Products", $"no category found: {categoryID}");
                return NotFound($"No category with id: {categoryID}");
            }

            Product? product = await _repo.GetProductForCategoryAsync(categoryID, productId);

            if (product == null)
            {
                return NotFound("product not found");
            }

            await _repo.DeleteProduct(product, true); //because we change the product object from db all we need is to save the changes to DB

            return NoContent();
        }
    }
}
