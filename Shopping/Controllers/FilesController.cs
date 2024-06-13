using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Shopping.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private FileExtensionContentTypeProvider _typeProvider;

        public FilesController(FileExtensionContentTypeProvider typeProvider)
        {
            _typeProvider = typeProvider ?? throw new ArgumentNullException(nameof(typeProvider));  // we add dependency injection for the file type service
        }

        [HttpGet("{path}")]
        public ActionResult GetFile(string path) 
        {
            //string path = "File1.pdf";
            if (!System.IO.File.Exists(path))
            {
                return NotFound();  
            }

            if (!_typeProvider.TryGetContentType(path, out string contentType)) //get the file content type 
            {
                contentType = "application/octet-stream"; //if not success get the type - set default
            }

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, contentType, Path.GetFileName(path));
        }
    }
}
