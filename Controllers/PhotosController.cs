using Microsoft.AspNetCore.Mvc;
using UniServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        [HttpGet("/photo")]
        public IActionResult Get(int photoId)
        {
            Photo pic = new Photo();
            pic= pic.readPhotoByPhotoId(photoId);
            if (pic.PhotoUri==null)
            {
                return NotFound(pic);
            }
            else
            {
               return Ok(pic);
            }
        }

        // POST api/<PhotosController>
        [HttpPost("/addPhoto")]
        public Photo Post([FromBody] Photo pic)
        {
            return pic.InsertPhoto(pic);
        }

    }
}
