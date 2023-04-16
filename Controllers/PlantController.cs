using Microsoft.AspNetCore.Mvc;
using UniServer.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet("/plant")]
        public List<Plant> Get()
        {
            Plant plant = new Plant();
            return plant.readAllPlant();

        }

        // GET api/<UsersController>/5
        [HttpGet("/plant/{id}")]
        public IActionResult Get(int id)
        {
            Plant plant = new Plant();
            plant= plant.readSpecificPlant(id);
            if (plant.PlantId==0)
            {
                return NotFound("not found in plant controller");
            }
            else
            {
                return Ok(plant);
            }

        }


        [HttpPost("/plant/{plantScientificName}")]
        public int Post(string plantScientificName)
        {
            Plant plant = new Plant();
            return plant.InsertPlant(plantScientificName);
        }



    }
}
