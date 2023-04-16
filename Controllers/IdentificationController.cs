using Microsoft.AspNetCore.Mvc;
using UniServer.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentificationController : ControllerBase
    {

        // GET api/<IdentificationController>/5
        [HttpGet("/identification/{id}")]
        public Identification Get(int id)
        {
            Identification identify = new Identification();
            return identify.readSpecificIdentification(id);

        }

        // GET api/<IdentificationController>/5
        [HttpPost("/CheckBadIdentification/{IdentificationId}")]
        public IActionResult CheckBadIdentification(int IdentificationId)
        {
            try
            {
                var obj = Identification.CheckIDE(IdentificationId);
                return Ok(obj);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
           

           

        }
        // GET api/<IdentificationController>/5
        [HttpGet("/getIdentifiedPlants/{id}")]
        public object GetPlants(int id)
        {
            Identification identify = new Identification();
            return identify.readAllPlantsByIdentificationId(id);
        }


        // POST api/<IdentificationController>
        [HttpPost("/identification")]
        public Identification Post([FromBody] Identification identify )
        {
            return identify.InsertIdentification(identify);
        }
        // POST api/<IdentificationController>
        [HttpPost("/plantIdentification")]
        public IActionResult postPlantIdentification(int plantId, int identificationId, double probability)
        {
            Identification identify = new Identification();
            int res= identify.postPlantIdentification(plantId, identificationId, probability);
            if (res>0)
            {
                return Ok("success");
            }
            else
            {
                return NotFound("Error in postPlantIdentification");
            }
        }
    }
}
