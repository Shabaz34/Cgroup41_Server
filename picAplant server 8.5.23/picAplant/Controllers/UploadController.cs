
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Microsoft.AspNetCore.Mvc;
using UniServer.Models;

namespace UniServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        // POST api/<UploadController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> files)
        {
            string path = Directory.GetCurrentDirectory();
            
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(path, "uploadedFiles/" + formFile.FileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    return Ok(filePath);

                }
            }
            return Ok(path);
        }
    }
}
