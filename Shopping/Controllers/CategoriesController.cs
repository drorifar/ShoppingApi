using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using System.Reflection.Metadata.Ecma335;

namespace Shopping.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController(ILogger<CategoriesController> _logger) : ControllerBase //default constructor in the class name. usefull when using DependencyInjecyion (new feature in C#)
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
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {           
            return Ok(MyDataStore.Current.Categories);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> GetCategory(int id)
        {
            // throw new Exception("ex ex ex");
            try
            {

                var result = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == id);

                if (result == null)
                {
                    _logger.LogWarning($"no category found: {id}");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while calling getCategories {id}", ex);

                return StatusCode(500, $"Exception while calling getCategories {id}");
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
