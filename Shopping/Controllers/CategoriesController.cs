using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Repositories;
using Shopping.services;
using System.Reflection.Metadata.Ecma335;

namespace Shopping.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController(ILogger<CategoriesController> _logger, 
        IMailService _mailService,
        ICategoryRepository _repo,
        IMapper _mapper) : ControllerBase //default constructor in the class name. usefull when using DependencyInjecyion (new feature in C#)
    {

        /// <summary>
        /// replace all this code with the ctor in the class name
        /// </summary>
        /// <returns></returns>
        //private ILogger<CategoriesController> _logger;

        //public CategoriesController(ILogger<CategoriesController> logger)
        //{
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //}


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryWithotProductDTO>>> GetCategories()
        {
            //return Ok(MyDataStore.Current.Categories); //before adding DB

            IEnumerable<Entities.Category> categories = await _repo.GetCategoriesAsync();

            // changed to mapper
            //List<CategoryWithotProductDTO> results = new List<CategoryWithotProductDTO>();
            //foreach (Entities.Category category in categories)
            //{
            //    results.Add(new CategoryWithotProductDTO()
            //    {
            //        ID = category.ID,
            //        Name = category.Name,
            //        Description = category.Description
            //    });
            //}

            var results = _mapper.Map<IEnumerable<CategoryWithotProductDTO>>(categories);

            return Ok(results);
        }

        // return IactionResult cause it can return different types
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id, bool includeProducts) // when we add param not in the get we add it as a param nin the url ?...
        {
            // throw new Exception("ex ex ex");

            try
            {
                //var result = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == id);

                //if (result == null)
                //{
                //    _logger.LogWarning($"no category found: {id}");
                //    _mailService.send("Missing category", $"no category found: {id}");
                //    return NotFound();
                //}

                //return Ok(result);

                var category = await _repo.GetCategoryAsync(id, includeProducts);

                if (category == null)
                {
                    _logger.LogWarning($"no category found: {id}");
                    _mailService.send("Missing category", $"no category found: {id}");
                    return NotFound();
                }

                if (includeProducts)
                {
                    return Ok(_mapper.Map<CategoryDTO>(category));
                }
                else
                {
                    return Ok(_mapper.Map<CategoryWithotProductDTO>(category));
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while calling getCategories {id}", ex);                
                return StatusCode(500, $"Exception while calling getCategories {id}"); //exemple of catching an error and return status code 500
            }
        }

        //[HttpGet("getCat")]
        //public JsonResult GetCategories()
        //{
        //    //return new JsonResult(new List<object>() {
        //    //new {
        //    //    Name = "cat 1",
        //    //    Id = 1
        //    //},
        //    //new  {
        //    //    Name = "cat 2",
        //    //    Id = 2
        //    //},
        //    //});
        //}
    }
}
