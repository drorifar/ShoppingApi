using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using System.Reflection.Metadata.Ecma335;

namespace Shopping.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {

 
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

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {           
            return Ok(MyDataStore.Current.Categories);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> GetCategory(int id)
        {
            var result = MyDataStore.Current.Categories.FirstOrDefault(c => c.ID == id);

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
    }
}
